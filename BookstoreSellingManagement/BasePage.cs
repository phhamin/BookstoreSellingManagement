using BLL;
using Bookstore;
using SubSonic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public class BasePage : System.Web.UI.Page
{
    protected string SortExpression
    {
        get
        {
            if (Session["SortExpression"] != null)
            {
                return Session["SortExpression"].ToString();
            }
            return "DefaultSortColumn"; // Điều chỉnh cột sắp xếp mặc định của bạn
        }
        set
        {
            Session["SortExpression"] = value;
        }
    }

    protected SortDirection SortDirection
    {
        get
        {
            if (Session["SortDirection"] != null)
            {
                return (SortDirection)Session["SortDirection"];
            }
            return SortDirection.Ascending; // Điều chỉnh hướng sắp xếp mặc định của bạn
        }
        set
        {
            Session["SortDirection"] = value;
        }
    }

    protected void ApplySorting<T>(GridView gridView, List<T> data)
    {
        string sortExpression = SortExpression;
        SortDirection sortDirection = SortDirection;

        PropertyInfo propertyInfo = typeof(T).GetProperty(sortExpression);

        if (propertyInfo != null)
        {
            if (sortDirection == SortDirection.Descending)
            {
                data = data.OrderByDescending(u => propertyInfo.GetValue(u, null)).ToList();
            }
            else
            {
                data = data.OrderBy(u => propertyInfo.GetValue(u, null)).ToList();
            }
        }

        gridView.DataSource = data;
        gridView.DataBind();
    }

    protected void SetSortDirection(string sortExpression)
    {
        if (SortExpression == sortExpression)
        {
            SortDirection = SortDirection == SortDirection.Ascending ? SortDirection.Descending : SortDirection.Ascending;
        }
        else
        {
            SortExpression = sortExpression;
            SortDirection = SortDirection.Ascending;
        }

    }
}
