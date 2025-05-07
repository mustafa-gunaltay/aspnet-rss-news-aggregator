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

                                // Görseli kontrol et
                                if (string.IsNullOrEmpty(imageUrl))
                                {
                                    imageUrl = "https://via.placeholder.com/800x400?text=Görsel+Yok";
                                }

                                ltNewsDetail.Text = $@"
                        <div class='article-header'>
                            <h1 class='article-title'>{title}</h1>
                            <div class='article-meta'>
                                <div>
                                    <span class='article-category'>{category}</span>
                                </div>
                                <div class='article-details'>
                                    <span><i class='fas fa-user'></i> {author}</span>
                                    <span><i class='fas fa-calendar'></i> {pubDate}</span>
                                </div>
                            </div>
                        </div>
                        <img src='{imageUrl}' alt='{title}' class='article-featured-img' onerror='this.src=""https://via.placeholder.com/800x400?text=Görsel+Yok""' />
                        <div class='article-content'>
                            <p>{desc}</p>
                        </div>";
                            }
                            else
                            {
                                ltNewsDetail.Text = @"
                        <div class='article-header'>
                            <h1 class='article-title'>Haber Bulunamadı</h1>
                        </div>
                        <div class='article-content'>
                            <p>Aradığınız haber içeriği sistemde bulunamadı.</p>
                        </div>";
                                logger.Warn($"NewsID={newsId} veritabanında bulunamadı.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"NewsID={newsId} için detay yüklenirken hata oluştu.");
                ltNewsDetail.Text = @"
        <div class='article-header'>
            <h1 class='article-title'>Hata Oluştu</h1>
        </div>
        <div class='article-content'>
            <p>Haber detayları yüklenirken bir sorun oluştu. Lütfen daha sonra tekrar deneyiniz.</p>
        </div>";
            }
        }
    }
}
