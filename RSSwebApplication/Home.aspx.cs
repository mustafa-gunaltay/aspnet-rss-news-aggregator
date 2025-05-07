using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Web.UI.WebControls;
using NLog;

namespace RSSwebApplication
{
    public partial class Home : System.Web.UI.Page
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    LoadNewsFromDatabase();
                    PopulateCategoryDropdown();
                    DisplayNews();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Home.aspx sayfasında Page_Load sırasında hata oluştu.");
                ltNewsList.Text = "Bir hata meydana geldi. Lütfen tekrar deneyin.";
            }
        }

        private void LoadNewsFromDatabase()
        {
            try
            {
                string dbPath = Server.MapPath("~/App_Data/RSSdb.accdb");
                string connStr = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath};Persist Security Info=False;";
                ArrayList newsList = new ArrayList();

                using (OleDbConnection conn = new OleDbConnection(connStr))
                {
                    conn.Open();
                    string query = "SELECT * FROM News";
                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        using (OleDbDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = Convert.ToInt32(reader["NewsID"]);
                                string title = reader["Title"].ToString();
                                string desc = reader["Description"].ToString();
                                string category = reader["Category"].ToString();
                                string author = reader["Author"].ToString();
                                string pubDate = reader["PubDate"].ToString();
                                string imageUrl = reader["ImageUrl"].ToString();

                                News news = new News(id, title, desc, category, author, pubDate, imageUrl);
                                newsList.Add(news);
                            }
                        }
                    }
                }

                Session["allNews"] = newsList;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Veritabanından haberler çekilirken hata oluştu.");
                throw; // üst katmana tekrar fırlatılıyor
            }
        }

        private void PopulateCategoryDropdown()
        {
            ddlCategory.Items.Clear();
            ddlCategory.Items.Add("Tüm Kategoriler");

            ArrayList newsList = (ArrayList)Session["allNews"];
            HashSet<string> categories = new HashSet<string>();

            foreach (News news in newsList)
            {
                if (!categories.Contains(news.Category))
                {
                    categories.Add(news.Category);
                    ddlCategory.Items.Add(news.Category);
                }
            }
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayNews();
        }

        private void DisplayNews()
        {
            ArrayList newsList = (ArrayList)Session["allNews"];
            string selectedCategory = ddlCategory.SelectedValue;

            string html = "<h3>Haber Listesi</h3>";
            foreach (News news in newsList)
            {
                if (selectedCategory != "Tüm Kategoriler" && news.Category != selectedCategory)
                    continue;

                html += $"<div style='margin-bottom:20px; border-bottom:1px solid #ccc;'>"
                      + $"<h4>{news.Title}</h4>"
                      + $"<p><strong>Tarih:</strong> {news.PubDate} | <strong>Yazar:</strong> {news.Author}</p>"
                      + $"<img src='{news.ImageUrl}' width='150'/><br/>"
                      + $"<p>{news.Description.Substring(0, Math.Min(200, news.Description.Length))}...</p>"
                      + $"<p><a href='NewsDetail.aspx?id={news.NewsID}'>Haberi detaylı oku →</a></p>"
                      + $"</div>";
            }

            ltNewsList.Text = html;
        }

    }
}
