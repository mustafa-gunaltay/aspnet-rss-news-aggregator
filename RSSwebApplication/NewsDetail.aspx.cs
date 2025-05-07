using System;
using System.Data.OleDb;
using NLog;

namespace RSSwebApplication
{
    public partial class NewsDetail : System.Web.UI.Page
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger(); // NLog logger

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    string idParam = Request.QueryString["id"];
                    if (!string.IsNullOrEmpty(idParam))
                    {
                        int newsId;
                        if (int.TryParse(idParam, out newsId))
                        {
                            LoadNewsDetail(newsId);
                        }
                        else
                        {
                            ltNewsDetail.Text = "Geçersiz haber ID.";
                            logger.Warn("QueryString'den alınan ID geçersiz: " + idParam);
                        }
                    }
                    else
                    {
                        ltNewsDetail.Text = "Haber ID belirtilmedi.";
                        logger.Warn("QueryString'de 'id' parametresi eksik.");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "NewsDetail.aspx Page_Load içinde beklenmeyen bir hata oluştu.");
                ltNewsDetail.Text = "Bir hata oluştu. Lütfen daha sonra tekrar deneyiniz.";
            }
        }

        private void LoadNewsDetail(int newsId)
        {
            try
            {
                string dbPath = Server.MapPath("~/App_Data/RSSdb.accdb");
                string connStr = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath};Persist Security Info=False;";

                using (OleDbConnection conn = new OleDbConnection(connStr))
                {
                    conn.Open();
                    string query = "SELECT * FROM News WHERE NewsID = ?";
                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("?", newsId);
                        using (OleDbDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string title = reader["Title"].ToString();
                                string desc = reader["Description"].ToString();
                                string category = reader["Category"].ToString();
                                string author = reader["Author"].ToString();
                                string pubDate = reader["PubDate"].ToString();
                                string imageUrl = reader["ImageUrl"].ToString();

                                ltNewsDetail.Text = $"<h2>{title}</h2>" +
                                    $"<p><strong>Kategori:</strong> {category} | <strong>Yazar:</strong> {author}</p>" +
                                    $"<p><strong>Tarih:</strong> {pubDate}</p>" +
                                    $"<img src='{imageUrl}' width='300'/><br/><br/>" +
                                    $"<p>{desc}</p>";
                            }
                            else
                            {
                                ltNewsDetail.Text = "Haber bulunamadı.";
                                logger.Warn($"NewsID={newsId} veritabanında bulunamadı.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"NewsID={newsId} için detay yüklenirken hata oluştu.");
                ltNewsDetail.Text = "Haber detayları yüklenemedi.";
            }
        }
    }
}
