using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Application.DtosCommon
{
    /// <summary>
    /// phân trang
    /// </summary>
   public class PagingRequestBase
    {
        /// <summary>
        /// Trang đầu
        /// </summary>
        public int pageIndex { get; set; }

        /// <summary>
        /// Tổng số trang
        /// </summary>
        public int pageSize { get; set; }
    }
}
