<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="QuanSinhTo.Quanly.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Css/bootstrap.min.css" rel="stylesheet" />
    <link href="Css/fontawesome.min.css" rel="stylesheet" />
    <title></title>
    <style type="text/css">
        html, body {
            background-image: url('http://getwallpapers.com/wallpaper/full/a/5/d/544750.jpg');
            background-size: cover;
            background-repeat: no-repeat;
            height: 100%;
            font-family: 'Numans', sans-serif;
        }

        .container {
            height: 100%;
            align-content: center;
        }

        .card {
            height: 370px;
            margin-top: auto;
            margin-bottom: auto;
            width: 400px;
            background-color: rgba(0,0,0,0.5) !important;
        }

        .social_icon span {
            font-size: 60px;
            margin-left: 10px;
            color: #FFC312;
        }

            .social_icon span:hover {
                color: white;
                cursor: pointer;
            }

        .card-header h3 {
            color: white;
        }

        .social_icon {
            position: absolute;
            right: 20px;
            top: -45px;
        }

        .input-group-prepend span {
            width: 50px;
            background-color: #FFC312;
            color: black;
            border: 0 !important;
        }

        input:focus {
            outline: 0 0 0 0 !important;
            box-shadow: 0 0 0 0 !important;
        }

        .remember {
            color: white;
        }

            .remember input {
                width: 20px;
                height: 20px;
                margin-left: 15px;
                margin-right: 5px;
            }

        .login_btn {
            color: black;
            background-color: #FFC312;
            width: 100px;
        }

            .login_btn:hover {
                color: black;
                background-color: white;
            }

        .links {
            color: white;
        }

            .links a {
                margin-left: 4px;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <div class="container pt-5">
            <div class="d-flex justify-content-center h-100">
                <div class="card">
                    <div class="card-header">
                        <h3>Đăng nhập</h3>
                    </div>
                    <div class="card-body">
                        <div class="input-group form-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text"><i class="fas fa-user"></i></span>
                            </div>
                            <asp:TextBox ID="txtTendangnhap" CssClass="form-control" placeholder="username" runat="server" />
                            <br />
                        </div>
                        <div class="input-group form-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text"><i class="fas fa-key"></i></span>
                            </div>
                            <asp:TextBox ID="txtPassword" CssClass="form-control" placeholder="password" TextMode="Password" runat="server" />
                        </div>
                        <div class="row align-items-center remember">
                            <input type="checkbox">Duy trì đăng nhập
				
                        </div>
                        <div class="form-group">
                            <asp:Button ID="btnLogin" CssClass="btn float-right login_btn" OnClick="btnLogin_Click" runat="server" Text="Đăng nhập" ValidationGroup="validateLogin" />
                            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="* Mật khẩu không được để trống." ControlToValidate="txtPassword" Display="Dynamic" ForeColor="Red" ValidationGroup="validateLogin" />
                            <br />
                            <asp:RequiredFieldValidator ID="rfvTendangnhap" runat="server" ErrorMessage="* Tên đăng nhập không được để trống." ControlToValidate="txtTendangnhap" Display="Dynamic" ForeColor="Red" ValidationGroup="validateLogin" />
                            <asp:Label ID="lblRegexDangnhap" runat="server" ForeColor="Red" />
                        </div>
                    </div>
                    <div class="card-footer">
                        <div class="d-flex justify-content-center links">
                            Don't have an account?<a href="#">Sign Up</a>
                        </div>
                        <div class="d-flex justify-content-center">
                            <a href="#">Forgot your password?</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
