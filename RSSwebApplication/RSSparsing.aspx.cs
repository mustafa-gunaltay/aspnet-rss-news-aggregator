using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using NLog;


namespace RSSwebApplication
{
    public partial class RSSparsing : Page
    {

        private static readonly Logger logger = LogManager.GetCurrentClassLogger(); // NLog tanımı
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string rssUrl = "https://rss.nytimes.com/services/xml/rss/nyt/Technology.xml";
                ArrayList newsList = new ArrayList();

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(rssUrl); // ← hata burada olabilir

                XmlNodeList items = xmlDoc.GetElementsByTagName("item");
                int idCounter = 1;

                foreach (XmlNode item in items)
                {
                    string title = item["title"]?.InnerText ?? "";
                    string description = item["description"]?.InnerText ?? "";
                    string category = item["category"]?.InnerText ?? "Technology";
                    string author = item["dc:creator"]?.InnerText ?? "Unknown";
                    string pubDate = item["pubDate"]?.InnerText ?? "";
                    string imageUrl = "";

                    XmlNode mediaNode = item.SelectSingleNode("media:content", GetMediaNamespaceManager(xmlDoc));
                    if (mediaNode != null && mediaNode.Attributes["url"] != null)
                    {
                        imageUrl = mediaNode.Attributes["url"].Value;
                    }

                    News news = new News(idCounter++, title, description, category, author, pubDate, imageUrl);
                    newsList.Add(news);
                }

                Session["newsList"] = newsList;

                ShowNewsFromSession();



                string dbPath = Server.MapPath("~/App_Data/RSSdb.accdb");
                string connStr = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath};Persist Security Info=False;";
                ArrayList newsListFromSession = (ArrayList)Session["newsList"];

                using (OleDbConnection conn = new OleDbConnection(connStr))
                {
                    conn.Open();
                    foreach (News news in newsListFromSession)
                    {
                        string checkQuery = "SELECT COUNT(*) FROM News WHERE Title = ?";
                        using (OleDbCommand checkCmd = new OleDbCommand(checkQuery, conn))
                        {
                            checkCmd.Parameters.AddWithValue("?", news.Title);
                            int count = (int)checkCmd.ExecuteScalar();
                            if (count > 0) continue;
                        }

                        string insertQuery = "INSERT INTO News (Title, Description, Category, Author, PubDate, ImageUrl) VALUES (?, ?, ?, ?, ?, ?)";
                        using (OleDbCommand insertCmd = new OleDbCommand(insertQuery, conn))
                        {
                            insertCmd.Parameters.AddWithValue("?", news.Title);
                            insertCmd.Parameters.AddWithValue("?", news.Description);
                            insertCmd.Parameters.AddWithValue("?", news.Category);
                            insertCmd.Parameters.AddWithValue("?", news.Author);
                            insertCmd.Parameters.AddWithValue("?", news.PubDate);
                            insertCmd.Parameters.AddWithValue("?", news.ImageUrl);

                            insertCmd.ExecuteNonQuery();
                        }
                    }
                }

                Response.Redirect("Home.aspx");
            }
            catch (Exception ex)
            {
                logger.Error(ex, "RSSparsing.aspx sayfasında beklenmeyen bir hata oluştu.");
                ltNewsOutput.Text = "Bir hata oluştu. Lütfen daha sonra tekrar deneyiniz.";
            }
        }

        private XmlNamespaceManager GetMediaNamespaceManager(XmlDocument doc)
        {
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("media", "http://search.yahoo.com/mrss/");
            return nsmgr;
        }

        private void ShowNewsFromSession()
        {
            if (Session["newsList"] != null)
            {
                ArrayList newsList = (ArrayList)Session["newsList"];
                string html = "<h3>Session'daki Haberler</h3><ul>";

                foreach (News news in newsList)
                {
                    html += $"<li><strong>{news.Title}</strong> - {news.PubDate}<br/>" +
                            $"{news.Description.Substring(0, Math.Min(100, news.Description.Length))}...<br/>" +
                            $"Kategori: {news.Category} - Yazar: {news.Author}<br/><img src='{news.ImageUrl}' width='100'/><hr/></li>";
                }

                html += "</ul>";
                ltNewsOutput.Text = html;
            }
            else
            {
                ltNewsOutput.Text = "Session'da haber bulunamadı.";
            }
        }
    }
}