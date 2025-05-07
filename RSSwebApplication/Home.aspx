<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="RSSwebApplication.Home" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TechVista</title>
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
            max-width: 1200px;
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

        .filter-section {
            background-color: white;
            padding: 15px;
            border-radius: 5px;
            margin: 20px 0;
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.05);
            display: flex;
            align-items: center;
        }

        .filter-label {
            font-weight: 600;
            margin-right: 15px;
            color: var(--primary-color);
        }

        .dropdown {
            padding: 10px;
            border: 1px solid var(--border-color);
            border-radius: 4px;
            font-size: 1rem;
            min-width: 200px;
            cursor: pointer;
            background-color: white;
            transition: border-color 0.3s;
        }

        .dropdown:focus {
            outline: none;
            border-color: var(--secondary-color);
        }

        .news-grid {
            display: grid;
            grid-template-columns: repeat(auto-fill, minmax(350px, 1fr));
            gap: 25px;
            margin-top: 30px;
        }

        .news-card {
            background-color: white;
            border-radius: 8px;
            overflow: hidden;
            box-shadow: 0 3px 10px rgba(0, 0, 0, 0.1);
            transition: transform 0.3s;
        }

        .news-card:hover {
            transform: translateY(-5px);
        }

        .news-img {
            height: 200px;
            width: 100%;
            object-fit: cover;
            border-bottom: 1px solid var(--border-color);
        }

        .news-content {
            padding: 20px;
        }

        .news-category {
            display: inline-block;
            background-color: var(--secondary-color);
            color: white;
            padding: 5px 10px;
            border-radius: 20px;
            font-size: 0.8rem;
            margin-bottom: 10px;
        }

        .news-title {
            font-size: 1.4rem;
            margin-bottom: 10px;
            color: var(--primary-color);
            line-height: 1.3;
        }

        .news-meta {
            display: flex;
            justify-content: space-between;
            color: #777;
            font-size: 0.9rem;
            margin-bottom: 15px;
        }

        .news-description {
            color: #555;
            margin-bottom: 20px;
            line-height: 1.5;
        }

        .read-more {
            display: inline-block;
            background-color: var(--primary-color);
            color: white;
            padding: 8px 15px;
            border-radius: 4px;
            text-decoration: none;
            font-weight: 500;
            transition: background-color 0.3s;
        }

        .read-more:hover {
            background-color: var(--secondary-color);
        }

        .read-more i {
            margin-left: 5px;
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
            .news-grid {
                grid-template-columns: 1fr;
            }

            .header-content {
                flex-direction: column;
                text-align: center;
            }

            .site-title {
                font-size: 1.8rem;
            }

            .filter-section {
                flex-direction: column;
                align-items: flex-start;
            }

            .filter-label {
                margin-bottom: 10px;
            }

            .dropdown {
                width: 100%;
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
            <div class="filter-section">
                <span class="filter-label">Kategoriye Göre Filtrele:</span>
                <asp:DropDownList ID="ddlCategory" runat="server" CssClass="dropdown" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                </asp:DropDownList>
            </div>

            <asp:Literal ID="ltNewsList" runat="server"></asp:Literal>
        </div>

        <footer class="footer">
            <div class="container">
                <p>&copy; <%= DateTime.Now.Year %> TechVista - All Rights Reserved</p>
            </div>
        </footer>
    </form>
</body>
</html>