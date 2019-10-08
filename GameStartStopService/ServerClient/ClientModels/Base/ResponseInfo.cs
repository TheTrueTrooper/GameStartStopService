using System;
using System.Collections;

namespace GameStartStopService.TheServerClient.ClientModels
{
    public class ResponseInfo<TData, TStatus>
    {
        public TStatus Status { get; set; }
        public TData Data { get; set; }
        public string Message { get; set; }
        public PageInfo PageInfo { get; set; }

        /// <summary>
        /// Method is used to create response object with given entity Type with Pagination info
        /// </summary>
        /// <param name="data">Type TData</param>
        /// <param name="status">Type TStatus</param>
        /// <param name="message">Return Message</param>
        /// <returns>ResponseInfo Object</returns>
        public ResponseInfo<TData, TStatus> GetResponse(TData data, TStatus status, string message)
        {
            var type = typeof(TData);
            var totalRecords = Counts(data);

            return new ResponseInfo<TData, TStatus>
            {
                Data = data,
                Status = status,
                Message = message,
                PageInfo = new PageInfo()
                {
                    NoOfPages = (int)Math.Ceiling(totalRecords * 1.0 / 10),
                    TotalRecords = totalRecords,
                    PageNumber = 1
                }
            };
        }

        private int Counts(object objectValue)
        {
            IEnumerable enumerable = objectValue as IEnumerable;
            int count = 0;
            if (enumerable != null)
            {
                foreach (object val in enumerable) count++;
            }
            return count;
        }
    }
}
