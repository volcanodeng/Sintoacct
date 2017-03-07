
// extend the 'telNum' rule
$.extend($.fn.validatebox.defaults.rules, {
    telNum: {
        validator: function (value, param) {
            return (/0\d{2,3}-\d{5,9}|0\d{2,3}-\d{5,9}/g).test(value);
        },
        message: '电话号码不匹配。'
    }
});

// extend the 'mobile' rule
$.extend($.fn.validatebox.defaults.rules, {
    mobile: {
        validator: function (value, param) {
            return (/^1[3|4|5|7|8]\d{9}$/g).test(value);
        },
        message: '手机号码无效。'
    }
});


// extend the 'equals' rule
$.extend($.fn.validatebox.defaults.rules, {
    equals: {
        validator: function (value, param) {
            return value == $(param[0]).val();
        },
        message: '输入内容不一致。'
    }
});

 


//单元格编辑
$.extend($.fn.datagrid.methods, {
    editCell: function (jq, param) {
        return jq.each(function () {
            var opts = $(this).datagrid('options');
            var fields = $(this).datagrid('getColumnFields', true).concat($(this).datagrid('getColumnFields'));
            for (var i = 0; i < fields.length; i++) {
                var col = $(this).datagrid('getColumnOption', fields[i]);
                col.editor1 = col.editor;
                if (fields[i] != param.field) {
                    col.editor = null;
                }
            }
            $(this).datagrid('beginEdit', param.index);
            var ed = $(this).datagrid('getEditor', param);
            if (ed) {
                if ($(ed.target).hasClass('textbox-f')) {
                    $(ed.target).textbox('textbox').focus();
                } else {
                    $(ed.target).focus();
                }
            }
            for (var i = 0; i < fields.length; i++) {
                var col = $(this).datagrid('getColumnOption', fields[i]);
                col.editor = col.editor1;
            }
        });
    },
    enableCellEditing: function (jq) {
        return jq.each(function () {
            var dg = $(this);
            var opts = dg.datagrid('options');
            opts.oldOnClickCell = opts.onClickCell;
            opts.onClickCell = function (index, field) {
                if (opts.editIndex != undefined) {
                    if (dg.datagrid('validateRow', opts.editIndex)) {
                        //自定义的赋值函数
                        if (typeof (fieldSettingFun) == "function") fieldSettingFun(opts.editIndex);
                        dg.datagrid('endEdit', opts.editIndex);
                        opts.editIndex = undefined;
                    } else {
                        return;
                    }
                }
                dg.datagrid('selectRow', index).datagrid('editCell', {
                    index: index,
                    field: field
                });
                opts.editIndex = index;
                opts.oldOnClickCell.call(this, index, field);
            }
        });
    }
});



//================配套easyui的方法=======

var tabWrapper = {
    tabId: "#tab_wrapper"
};

//以插入内容的方式增加tab页
function AddTabContent(title, content) {
    if ($(tabWrapper.tabId).tabs("exists", title)) {
        $(tabWrapper.tabId).tabs("select", title);
        return;
    }

    $(tabWrapper.tabId).tabs('add', {
        title: title,
        content: content,
        closable: true,
        selected: true
    });
}

//以链接方式插入tab页
function AddTabHref(title, href) {

    if ($(tabWrapper.tabId).tabs("exists", title)) {
        $(tabWrapper.tabId).tabs("select", title);
        return;
    }

    $(tabWrapper.tabId).tabs('add', {
        title: title,
        href: href,
        closable: true,
        selected: true
    });

}

//以新增Iframe方式插入tab页
function AddTabIframe(title, href) {

    if ($(tabWrapper.tabId).tabs("exists", title)) {
        $(tabWrapper.tabId).tabs("select", title);
        return;
    }

    $(tabWrapper.tabId).tabs('add', {
        title: title,
        content: '<iframe scrolling="yes" frameborder="0"  src="' + href + '" style="width:100%;height:100%;"></iframe>',
        closable: true,
        selected: true
    });

    //隐藏tab滚动条避免多重滚动条
    $(".tabs-panels .panel-body").css({ "overflow": "hidden" });
}

//关闭当前选中的Tab页
function removePanel() {
    var tab = $(tabWrapper.tabId).tabs('getSelected');
    if (tab) {
        var index = $(tabWrapper.tabId).tabs('getTabIndex', tab);
        $(tabWrapper.tabId).tabs('close', index);
    }
}



/**
 * Easyui Datagrid Row Editing
 */

//封装datagrid的编辑索引及对象
var dgWrapper = {
    editIndex: undefined,
    dg: undefined
}

//结束行编辑
function endEditing() {
    if (dgWrapper.editIndex == undefined) { return true }
    if (dgWrapper.dg.datagrid('validateRow', dgWrapper.editIndex)) {
        //自定义的赋值函数
        if (typeof (fieldSettingFun) == "function") fieldSettingFun(dgWrapper.editIndex, dgWrapper.dg);

        dgWrapper.dg.datagrid('endEdit', dgWrapper.editIndex);
        dgWrapper.editIndex = undefined;
        return true;
    } else {
        return false;
    }
}
//点击单元格事件
function onClickCell(index, field) {
    if (!dgWrapper.dg) dgWrapper.dg = $(this);
    if (dgWrapper.editIndex != index) {
        if (endEditing()) {
            dgWrapper.dg.datagrid('selectRow', index).datagrid('beginEdit', index);

            var ed = dgWrapper.dg.datagrid('getEditor', { index: index, field: field });
            if (ed) {
                ($(ed.target).data('textbox') ? $(ed.target).textbox('textbox') : $(ed.target)).focus();
                ($(ed.target).data('textbox') ? $(ed.target).textbox('textbox') : $(ed.target)).select();
            }
            dgWrapper.editIndex = index;
        } else {
            dgWrapper.dg.datagrid('selectRow', dgWrapper.editIndex);
        }
    }
}




//==========公共脚本方法==========

//Web Api调用返回结果处理
function responseHandle(data) {
    if (data == null) return false;
    if (typeof data != "object") data = JSON.parse(data);

    if (!data.IsSuccess) {
        $.messager.alert("操作结果", data.message, "warning");
        return false;
    }
    else {
        $.messager.show({ title: "操作结果", msg: "操作成功完成" });
        return true;
    }
}