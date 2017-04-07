using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sintoacct.Ledger.Models
{
    public class DatagridViewModel<T> 
    {
        public int total { get { return rows.Count; } }

        public List<T> rows { get; set; }
    }

    public class TreeViewModel<T> 
    {
        public TreeViewModel()
        {
            children = new List<TreeViewModel<T>>();
        }

        /// <summary>
        /// node id, which is important to load remote data
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// node text to show
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// node state, 'open' or 'closed', default is 'open'. When set to 'closed', the node have children nodes and will load them from remote site
        /// </summary>
        public string state { get; set; }

        /// <summary>
        ///  Indicate whether the node is checked selected
        /// </summary>
        public bool @checked { get; set; }

        /// <summary>
        ///  custom attributes can be added to a node
        /// </summary>
        public T attributes { get; set; }

        /// <summary>
        /// an array nodes defines some children nodes
        /// </summary>
        public List<TreeViewModel<T>> children { get; set; }

    }

    public class ComboboxViewModel
    {
        public string val { get; set; }

        public string text { get; set; }
    }
}