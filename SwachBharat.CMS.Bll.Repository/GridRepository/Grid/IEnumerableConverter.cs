using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Xml.Linq;
using System.Web;
using System.Linq.Dynamic;
using System.Web.Script.Serialization;

namespace SwachBharat.CMS.Bll.Repository.GridRepository.Grid
{
    public static class IEnumerableConverter
    {

        /// <summary>
        /// Extension method for db dataset (IEnumerable) to convert to XML form for jqGrid.
        /// Applies any toolbar filters before converting to XML.
        /// On a grid, if the user clicks the 'search' button, input fields will appear above each column (toolbar filters)
        /// Entering a value and pressing enter will set _search to true and send the values in the query string, which is eventually passed here.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataSet"></param>
        /// <param name="sord">sorting order: ascending or descending</param>
        /// <param name="page">the requested page number</param>
        /// <param name="rows">the number of rows to fetch (rows per page)</param>
        /// <param name="_search">whether or not a search was performed</param>
        /// <param name="QueryString">The query string from the Ajax call in the grid definition.</param>
        /// <param name="sidx">the sort index. name of the column to order by. If not set, default to column 1.</param>
        /// <returns></returns>
        public static XDocument GetJqGridXML<T>(this IEnumerable<T> dataSet, string sord, int page, int rows, bool _search, NameValueCollection QueryString, string sidx = "1")
        {
            // Get the total number of rows
            int count = 0;
            if (dataSet != null)
                count = dataSet.Count();

            // If the dataset is empty, return an empty XML doc.
            // If this is removed and the flow is allowed to continue with count = 0, there will be exceptions.
            if (count == 0) return new XDocument();

            // Get the total number of pages, round up if decimal
            int totalPages = 0;
            if (rows > 0)
            {
                totalPages = Convert.ToInt32(Math.Ceiling((double)count / rows));
            }

            // If the requested page is higher than the total pages, set it equal to the last page
            if (page > totalPages) page = totalPages;

            // If the requested page is less than 1, set it equal to 1
            if (page < 1) page = 1;

            // Get all the property names for these objects
            var properties = dataSet.ElementAt(0).GetType().GetProperties();

            // Find and Apply filters.
            if (_search)
            {
                // Cycle through all the object's properties (corresponds to columns of the table)
                foreach (var property in properties)
                {
                    string columnName = property.Name;
                    string searchString = QueryString[property.Name.ToLower()];

                    // See if there is a search value that has been passed through the query string.
                    if (searchString != null)
                    {
                        var whereString = String.Empty;

                        // If the property is a string, use a "startsWith" search.
                        if (property.PropertyType.Name == "String")
                        {
                            whereString = String.Format("{0}.StartsWith(\"{1}\")", columnName, searchString);
                        }

                        // If the property is not a string, use an 'equals' search
                        else
                        {
                            whereString = String.Format("{0}={1}", columnName, searchString);
                        }

                        // Pass the whereString variable to a Dynamic LINQ statement: thanks to Microsoft's
                        // Dynamic LINQ library.
                        dataSet = dataSet.AsQueryable().Where((whereString));
                    }
                }
            }

            // Order by the chosen column, relies on Dynamic LINQ library
            foreach (var property in properties)
            {
                // Sidx is the column that was clicked to sort either asc or desc, parsed from the query string.
                if (sidx == property.Name.ToLower())
                {
                    dataSet = dataSet.AsQueryable().OrderBy(property.Name);
                }
            }

            // Order by (above) defaults to asc, if desc is selected, reverse the list order
            if (sord == "desc")
            {
                dataSet = dataSet.Reverse();
            }

            // Get the rows for the result. Only the rows for the given page. Rows = number of rows per page.
            dataSet = dataSet.Skip((page - 1) * rows).Take(rows);

            // Generate the XML document and insert the rows. XML is formatted for jqGrid digestion.
            int i = 0; // Counter for the row ids.
            XDocument XML = new XDocument(
                new XElement("rows",
                    new XElement("page", page),
                    new XElement("total", totalPages),
                    new XElement("records", count),
                        // Rows
                        from d in dataSet
                        select
                        new XElement("row", new XAttribute("id", ++i),
                            from p in properties
                            select new XElement("cell", p.GetValue(d, null))
                        )
                    )
                );

            return XML;
        }

        public static string GetJqGridJson<T>(this IEnumerable<T> dataSet, string sord, int page, int rows, bool _search, NameValueCollection QueryString, string sidx = "1")
        {
            // If the dataset is empty, return an empty XML doc.
            // If this is removed and the flow is allowed to continue with count = 0, there will be exceptions.
            if (dataSet == null || dataSet.Count() == 0)
                return "{\"page\":1,\"total\":0,\"records\":0,\"rows\":[]}";

            // Get all the property names for these objects
            var properties = dataSet.ElementAt(0).GetType().GetProperties();

            // Find and Apply filters.
            if (_search)
            {
                // Cycle through all the object's properties (corresponds to columns of the table)
                foreach (var property in properties)
                {
                    string columnName = property.Name;
                    string searchString = QueryString[property.Name.ToLower()];

                    // See if there is a search value that has been passed through the query string.

                    //if (searchString != null && columnName != "id")
                    if (searchString != null && columnName != "id" && !columnName.Contains("date"))
                    {
                        string whereString = String.Empty;

                        // If the property is a string, use a "startsWith" search.
                        if (property.PropertyType.Name == "String")
                        {
                            whereString = String.Format("{0}.StartsWith(\"{1}\")", columnName, searchString);
                        }
                        // If the property is a string, use a "startsWith" search.
                        else if (property.PropertyType.Name == "Nullable`1")
                        {
                            //whereString = "";
                            whereString = String.Format("{0}={1}", columnName, searchString);
                        }
                        // If the property is not a string, use an 'equals' search
                        else
                        {
                            whereString = String.Format("{0}={1}", columnName, searchString);
                        }

                        // Pass the whereString variable to a Dynamic LINQ statement: thanks to Microsoft's
                        // Dynamic LINQ library.
                        if (whereString != "")
                            dataSet = dataSet.AsQueryable().Where(whereString);
                    }
                }
            }

            // Order by the chosen column, relies on Dynamic LINQ library
            foreach (var property in properties)
            {
                // Sidx is the column that was clicked to sort either asc or desc, parsed from the query string.
                if (sidx.ToLower() == property.Name.ToLower())
                {
                    dataSet = dataSet.AsQueryable().OrderBy(property.Name);
                }
            }

            // Order by (above) defaults to asc, if desc is selected, reverse the list order
            if (sord == "desc")
            {
                dataSet = dataSet.Reverse();
            }

            // Get the total number of rows
            int count = dataSet.Count();

            // Get the total number of pages, round up if decimal
            int totalPages = 0;
            if (rows > 0)
            {
                totalPages = Convert.ToInt32(Math.Ceiling((double)count / rows));
            }

            // If the requested page is higher than the total pages, set it equal to the last page
            if (page > totalPages)
                page = totalPages;

            // If the requested page is less than 1, set it equal to 1
            if (page < 1)
                page = 1;


            // Get the rows for the result. Only the rows for the given page. Rows = number of rows per page.
            dataSet = dataSet.Skip((page - 1) * rows).Take(rows);

            int c = 0;
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            // This line was inserted to keep the serializer from getting a length error
            serializer.MaxJsonLength = Int32.MaxValue;

            var result = serializer.Serialize(new
            {

                page = page,
                total = totalPages,
                records = count,
                rows = from d in dataSet
                       select new
                       {
                           id = ++c,
                           cell = from p in properties
                                  select p.GetValue(d, null)
                       }
            }); 
            return result;
        }

        public static bool CheckDate(String date)
        {
            try
            {
                DateTime dt = DateTime.Parse(date);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Takes the given dataset and filename, converts the dataset into CSV form and constructs an output
        /// file to the Http response object.  It will appear as a download in the user's browser (FileName.csv)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="DataSet"></param>
        /// <param name="FileName"></param>
        /// <param name="Response"></param>
        public static void ToCSV<T>(this IEnumerable<T> DataSet, string FileName, HttpResponseBase Response)
        {
            StringBuilder CSVContent = new StringBuilder();
            // Get a collection of object properties from the first object in the collection.
            var properties = from p in DataSet.ElementAt(0).GetType().GetProperties() select p;

            // Use the property names as column headers. First row of the CSV.
            foreach (var property in properties)
            {
                CSVContent.AppendFormat("{0},", property.Name);
            }
            CSVContent.Append("\r");

            // Cycle through each object in the dataSet
            foreach (var row in DataSet)
            { 
                foreach (var property in properties)
                {
                    CSVContent.AppendFormat("{0},", property.GetValue(row, null));
                } 
                CSVContent.Append("\r");
            }
             
            Response.Clear();
            Response.AddHeader("Content-Disposition", String.Format("attachment; filename={0}.csv", FileName));
            Response.ContentType = "text/csv";
            Response.Write(CSVContent);
            Response.End();
        }

        public static string GetDataTableJson<T>(this IEnumerable<T> dataSet, string sortColumn, string sortColumnDir, string draw, string length, string searchValue, string start)
        {

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                dataSet = dataSet.OrderBy(sortColumn + " " + sortColumnDir);
            }

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            recordsTotal = dataSet.Count();
            //Paging   
            //if (pageSize == 100)
            //{
            //    pageSize = recordsTotal;
            //}
            var data = dataSet.Skip(skip).Take(pageSize).ToList();
            //Returning Json Data  
            JavaScriptSerializer serializer = new JavaScriptSerializer(); 
            // This line was inserted to keep the serializer from getting a length error
            serializer.MaxJsonLength = Int32.MaxValue; 
            var result = serializer.Serialize(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            return result;
        }
    }
}
