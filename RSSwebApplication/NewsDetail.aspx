<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsDetail.aspx.cs" Inherits="RSSwebApplication.NewsDetail" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TechVista - News Detail</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet" />
    <style>
        :root {
            --primary-color: #2c3e50;
            --secondary-color: #3498db;
            --accent-color: #e74c3c;
            --text-color: #333;
            --light-bg: #f4f4f4;
            --border-color: #ddd;
        }

        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }

        body {
            background-color: var(--light-bg);
            color: var(--text-color);
            line-height: 1.6;
        }

        .container {
            max-width: 1000px;
            margin: 0 auto;
            padding: 20px;
        }

        header {
            background-color: var(--primary-color);
            color: white;
            padding: 20px 0;
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
        }

        .header-content {
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

        .site-title {
            font-size: 2.2rem;
            font-weight: 700;
            text-transform: uppercase;
            letter-spacing: 1px;
        }

        .site-slogan {
            font-size: 1rem;
            font-weight: normal;
            margin-top: 5px;
            opacity: 0.8;
        }

        .article {
            background-color: white;
            border-radius: 8px;
            box-shadow: 0 3px 10px rgba(0, 0, 0, 0.1);
            padding: 30px;
            margin-top: 30px;
        }

        .article-header {
            margin-bottom: 25px;
        }

        .article-title {
            font-size: 2.2rem;
            color: var(--primary-color);
            margin-bottom: 15px;
            line-height: 1.3;
        }

        .article-meta {
            display: flex;
            justify-content: space-between;
            flex-wrap: wrap;
            color: #777;
            font-size: 0.95rem;
            margin-bottom: 20px;
            padding-bottom: 20px;
            border-bottom: 1px solid var(--border-color);
        }

        .article-category {
            display: inline-block;
            background-color: var(--secondary-color);
            color: white;
            padding: 5px 12px;
            border-radius: 20px;
            font-size: 0.9rem;
            margin-right: 15px;
        }

        .article-details {
            display: flex;
            align-items: center;
        }

        .article-details span {
            margin-right: 20px;
        }

        .article-details i {
            margin-right: 6px;
            color: var(--secondary-color);
        }

        .article-featured-img {
            width: 100%;
            max-height: 500px;
            object-fit: cover;
            border-radius: 6px;
            margin-bottom: 25px;
        }

        .article-content {
            font-size: 1.1rem;
            line-height: 1.8;
            color: #444;
        }

        .article-content p {
            margin-bottom: 20px;
        }

        .back-button {
            display: inline-block;
            background-color: var(--primary-color);
            color: white;
            padding: 10px 20px;
            border-radius: 4px;
            text-decoration: none;
            font-weight: 500;
            margin-top: 30px;
            transition: background-color 0.3s;
        }

        .back-button:hover {
            background-color: var(--secondary-color);
        }

        .back-button i {
            margin-right: 8px;
        }

        .footer {
            background-color: var(--primary-color);
            color: white;
            text-align: center;
            padding: 20px;
            margin-top: 40px;
            font-size: 0.9rem;
        }

        @media screen and (max-width: 768px) {
            .header-content {
                flex-direction: column;
                text-align: center;
            }

            .site-title {
                font-size: 1.8rem;
            }

            .article {
                padding: 20px;
            }

            .article-title {
                font-size: 1.8rem;
            }

            .article-meta {
                flex-direction: column;
            }

            .article-details {
                margin-top: 10px;
                flex-wrap: wrap;
            }

            .article-details span {
                margin-bottom: 8px;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <header>
            <div class="container header-content">
                <div>
                    <h1 class="site-title">TechVista</h1>
                    <p class="site-slogan">Latest news from the world of technology</p>
                </div>
            </div>
        </header>

        <div class="container">
            <div class="article">
                <asp:Literal ID="ltNewsDetail" runat="server"></asp:Literal>
            </div>
            
            <a href="Home.aspx" class="back-button"><i class="fas fa-arrow-left"></i> Haberlere Geri Dön</a>
        </div>

        <footer class="footer">
            <div class="container">
                <p>&copy; <%= DateTime.Now.Year %> TechVista - All Rights Reserved</p>
            </div>
        </footer>
    </form>
</body>
</html>