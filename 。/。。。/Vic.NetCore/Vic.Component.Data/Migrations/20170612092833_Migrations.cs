using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Vic.Core.Data.Migrations
{
    public partial class Migrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_CMS_Article",
                columns: table => new
                {
                    FID = table.Column<Guid>(maxLength: 32, nullable: false),
                    FArticleTypeID = table.Column<Guid>(nullable: false),
                    FAuthor = table.Column<string>(maxLength: 20, nullable: true),
                    FBrowseNum = table.Column<int>(nullable: false),
                    FContent = table.Column<string>(type: "text", nullable: true),
                    FCreateTime = table.Column<DateTime>(nullable: false),
                    FCreatorID = table.Column<Guid>(maxLength: 32, nullable: false),
                    FDescription = table.Column<string>(maxLength: 800, nullable: true),
                    FImages = table.Column<string>(maxLength: 200, nullable: true),
                    FLanguageTypeID = table.Column<Guid>(maxLength: 32, nullable: false),
                    FLastUpdateTime = table.Column<DateTime>(nullable: true),
                    FLastUpdateUserID = table.Column<Guid>(maxLength: 32, nullable: true),
                    FPublishDate = table.Column<DateTime>(type: "DateTime", nullable: false),
                    FRemark = table.Column<string>(maxLength: 200, nullable: true),
                    FSeoTitle = table.Column<string>(maxLength: 100, nullable: true),
                    FSerialNumber = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FSource = table.Column<string>(maxLength: 20, nullable: true),
                    FTitle = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_CMS_Article", x => x.FID);
                });

            migrationBuilder.CreateTable(
                name: "T_CMS_ArticleType",
                columns: table => new
                {
                    FID = table.Column<Guid>(maxLength: 32, nullable: false),
                    FCreateTime = table.Column<DateTime>(nullable: false),
                    FCreatorID = table.Column<Guid>(maxLength: 32, nullable: false),
                    FImages = table.Column<string>(maxLength: 200, nullable: true),
                    FLanguageTypeID = table.Column<Guid>(maxLength: 32, nullable: false),
                    FLastUpdateTime = table.Column<DateTime>(nullable: true),
                    FLastUpdateUserID = table.Column<Guid>(maxLength: 32, nullable: true),
                    FSerialNumber = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FTitle = table.Column<string>(maxLength: 100, nullable: true),
                    FUrl = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_CMS_ArticleType", x => x.FID);
                });

            migrationBuilder.CreateTable(
                name: "T_CMS_Banner",
                columns: table => new
                {
                    FID = table.Column<Guid>(maxLength: 32, nullable: false),
                    FCreateTime = table.Column<DateTime>(nullable: false),
                    FCreatorID = table.Column<Guid>(maxLength: 32, nullable: false),
                    FImage = table.Column<string>(maxLength: 200, nullable: true),
                    FLanguageTypeID = table.Column<Guid>(maxLength: 32, nullable: false),
                    FLastUpdateTime = table.Column<DateTime>(nullable: true),
                    FLastUpdateUserID = table.Column<Guid>(maxLength: 32, nullable: true),
                    FSerialNumber = table.Column<int>(nullable: false),
                    FTitle = table.Column<string>(maxLength: 100, nullable: true),
                    FUrl = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_CMS_Banner", x => x.FID);
                });

            migrationBuilder.CreateTable(
                name: "T_PST_CustomerUser",
                columns: table => new
                {
                    FID = table.Column<Guid>(maxLength: 32, nullable: false),
                    FAuthenticationVerify = table.Column<int>(nullable: false),
                    FCardFrontPhoto = table.Column<string>(maxLength: 100, nullable: true),
                    FCardHandHeldPhoto = table.Column<string>(maxLength: 100, nullable: true),
                    FCardNegativePhoto = table.Column<string>(maxLength: 100, nullable: true),
                    FCreateTime = table.Column<DateTime>(nullable: false),
                    FCreatorID = table.Column<Guid>(maxLength: 32, nullable: false),
                    FEmail = table.Column<string>(maxLength: 50, nullable: true),
                    FIDCard = table.Column<string>(maxLength: 18, nullable: true),
                    FLastUpdateTime = table.Column<DateTime>(nullable: true),
                    FLastUpdateUserID = table.Column<Guid>(maxLength: 32, nullable: true),
                    FMobile = table.Column<string>(maxLength: 20, nullable: true),
                    FNickName = table.Column<string>(maxLength: 20, nullable: true),
                    FPassWord = table.Column<string>(maxLength: 200, nullable: true),
                    FRealName = table.Column<string>(maxLength: 20, nullable: true),
                    FRecommendID = table.Column<Guid>(maxLength: 32, nullable: false),
                    FStatus = table.Column<int>(nullable: false),
                    FTPassWord = table.Column<string>(maxLength: 200, nullable: true),
                    FTradeAccount = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FType = table.Column<int>(nullable: false),
                    FVerifyResult = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_PST_CustomerUser", x => x.FID);
                });

            migrationBuilder.CreateTable(
                name: "T_AM_Department",
                columns: table => new
                {
                    FID = table.Column<Guid>(maxLength: 32, nullable: false),
                    FCode = table.Column<string>(maxLength: 20, nullable: false),
                    FContactNumber = table.Column<string>(maxLength: 20, nullable: true),
                    FCreateTime = table.Column<DateTime>(nullable: false),
                    FCreatorID = table.Column<Guid>(maxLength: 32, nullable: false),
                    FIsDeleted = table.Column<bool>(nullable: false),
                    FLastUpdateTime = table.Column<DateTime>(nullable: true),
                    FLastUpdateUserID = table.Column<Guid>(maxLength: 32, nullable: true),
                    FManager = table.Column<string>(maxLength: 50, nullable: true),
                    FName = table.Column<string>(maxLength: 50, nullable: false),
                    FParentId = table.Column<Guid>(maxLength: 32, nullable: false),
                    FRemarks = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_AM_Department", x => x.FID);
                });

            migrationBuilder.CreateTable(
                name: "T_PL_LanguageType",
                columns: table => new
                {
                    FID = table.Column<Guid>(maxLength: 32, nullable: false),
                    FCode = table.Column<string>(maxLength: 50, nullable: true),
                    FCreateTime = table.Column<DateTime>(nullable: false),
                    FCreatorID = table.Column<Guid>(maxLength: 32, nullable: false),
                    FImages = table.Column<string>(maxLength: 200, nullable: true),
                    FLastUpdateTime = table.Column<DateTime>(nullable: true),
                    FLastUpdateUserID = table.Column<Guid>(maxLength: 32, nullable: true),
                    FTitle = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_PL_LanguageType", x => x.FID);
                });

            migrationBuilder.CreateTable(
                name: "T_PL_MailConfig",
                columns: table => new
                {
                    FID = table.Column<Guid>(maxLength: 32, nullable: false),
                    FAccountName = table.Column<string>(maxLength: 50, nullable: false),
                    FAccountPwd = table.Column<string>(maxLength: 200, nullable: false),
                    FCallEnumKey = table.Column<string>(maxLength: 50, nullable: false),
                    FContent = table.Column<string>(maxLength: 8000, nullable: false),
                    FCreateTime = table.Column<DateTime>(nullable: false),
                    FCreatorID = table.Column<Guid>(maxLength: 32, nullable: false),
                    FIsDefault = table.Column<bool>(nullable: false),
                    FLastUpdateTime = table.Column<DateTime>(nullable: true),
                    FLastUpdateUserID = table.Column<Guid>(maxLength: 32, nullable: true),
                    FMailFrom = table.Column<string>(maxLength: 50, nullable: false),
                    FMailTo = table.Column<string>(maxLength: 50, nullable: false),
                    FNickName = table.Column<string>(maxLength: 50, nullable: false),
                    FPort = table.Column<short>(maxLength: 10, nullable: false),
                    FStmp = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_PL_MailConfig", x => x.FID);
                });

            migrationBuilder.CreateTable(
                name: "T_PL_MailTemplate",
                columns: table => new
                {
                    FID = table.Column<Guid>(maxLength: 32, nullable: false),
                    FCallEnumKey = table.Column<string>(maxLength: 50, nullable: false),
                    FContent = table.Column<string>(maxLength: 8000, nullable: false),
                    FCreateTime = table.Column<DateTime>(nullable: false),
                    FCreatorID = table.Column<Guid>(maxLength: 32, nullable: false),
                    FIsDefault = table.Column<bool>(nullable: false),
                    FLanguageTypeID = table.Column<Guid>(maxLength: 32, nullable: false),
                    FLastUpdateTime = table.Column<DateTime>(nullable: true),
                    FLastUpdateUserID = table.Column<Guid>(maxLength: 32, nullable: true),
                    FMailTitle = table.Column<string>(maxLength: 500, nullable: false),
                    FTitle = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_PL_MailTemplate", x => x.FID);
                });

            migrationBuilder.CreateTable(
                name: "T_AM_Menu",
                columns: table => new
                {
                    FID = table.Column<Guid>(maxLength: 32, nullable: false),
                    FCode = table.Column<string>(maxLength: 20, nullable: false),
                    FCreateTime = table.Column<DateTime>(nullable: false),
                    FCreatorID = table.Column<Guid>(maxLength: 32, nullable: false),
                    FIcon = table.Column<string>(maxLength: 50, nullable: true),
                    FIsTopMenu = table.Column<bool>(nullable: false),
                    FLastUpdateTime = table.Column<DateTime>(nullable: true),
                    FLastUpdateUserID = table.Column<Guid>(maxLength: 32, nullable: true),
                    FName = table.Column<string>(maxLength: 50, nullable: false),
                    FParentId = table.Column<Guid>(maxLength: 32, nullable: false),
                    FRemarks = table.Column<string>(maxLength: 200, nullable: true),
                    FSerialNumber = table.Column<int>(nullable: false),
                    FType = table.Column<int>(nullable: false),
                    FUrl = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_AM_Menu", x => x.FID);
                });

            migrationBuilder.CreateTable(
                name: "T_PL_PlateSetting",
                columns: table => new
                {
                    FID = table.Column<Guid>(maxLength: 32, nullable: false),
                    FCreateTime = table.Column<DateTime>(nullable: false),
                    FCreatorID = table.Column<Guid>(maxLength: 32, nullable: false),
                    FKey = table.Column<string>(maxLength: 50, nullable: false),
                    FLastUpdateTime = table.Column<DateTime>(nullable: true),
                    FLastUpdateUserID = table.Column<Guid>(maxLength: 32, nullable: true),
                    FTag = table.Column<string>(maxLength: 50, nullable: false),
                    FValue = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_PL_PlateSetting", x => x.FID);
                });

            migrationBuilder.CreateTable(
                name: "T_PL_PushService",
                columns: table => new
                {
                    FID = table.Column<Guid>(maxLength: 32, nullable: false),
                    FCreateTime = table.Column<DateTime>(nullable: false),
                    FCreatorID = table.Column<Guid>(maxLength: 32, nullable: false),
                    FIP = table.Column<string>(maxLength: 20, nullable: false),
                    FLastUpdateTime = table.Column<DateTime>(nullable: true),
                    FLastUpdateUserID = table.Column<Guid>(maxLength: 32, nullable: true),
                    FPort = table.Column<int>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_PL_PushService", x => x.FID);
                });

            migrationBuilder.CreateTable(
                name: "T_AM_Role",
                columns: table => new
                {
                    FID = table.Column<Guid>(maxLength: 32, nullable: false),
                    FCode = table.Column<string>(maxLength: 20, nullable: false),
                    FCreateTime = table.Column<DateTime>(nullable: false),
                    FCreatorID = table.Column<Guid>(maxLength: 32, nullable: false),
                    FLastUpdateTime = table.Column<DateTime>(nullable: true),
                    FLastUpdateUserID = table.Column<Guid>(maxLength: 32, nullable: true),
                    FName = table.Column<string>(maxLength: 50, nullable: false),
                    FRemarks = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_AM_Role", x => x.FID);
                });

            migrationBuilder.CreateTable(
                name: "T_PL_ShortMsgConfig",
                columns: table => new
                {
                    FID = table.Column<Guid>(maxLength: 32, nullable: false),
                    FAccountName = table.Column<string>(maxLength: 50, nullable: true),
                    FAccountPwd = table.Column<string>(maxLength: 200, nullable: true),
                    FApiKey = table.Column<string>(maxLength: 200, nullable: true),
                    FApiSecret = table.Column<string>(maxLength: 200, nullable: true),
                    FApiUrl = table.Column<string>(maxLength: 200, nullable: false),
                    FCallEnumKey = table.Column<string>(maxLength: 50, nullable: true),
                    FCreateTime = table.Column<DateTime>(nullable: false),
                    FCreatorID = table.Column<Guid>(maxLength: 32, nullable: false),
                    FIsDefault = table.Column<bool>(nullable: false),
                    FLastUpdateTime = table.Column<DateTime>(nullable: true),
                    FLastUpdateUserID = table.Column<Guid>(maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_PL_ShortMsgConfig", x => x.FID);
                });

            migrationBuilder.CreateTable(
                name: "T_PL_ShortMsgTemplate",
                columns: table => new
                {
                    FID = table.Column<Guid>(maxLength: 32, nullable: false),
                    FCallEnumKey = table.Column<string>(maxLength: 50, nullable: false),
                    FContent = table.Column<string>(maxLength: 8000, nullable: false),
                    FCreateTime = table.Column<DateTime>(nullable: false),
                    FCreatorID = table.Column<Guid>(maxLength: 32, nullable: false),
                    FIsDefault = table.Column<bool>(nullable: false),
                    FLanguageTypeID = table.Column<Guid>(maxLength: 32, nullable: false),
                    FLastUpdateTime = table.Column<DateTime>(nullable: true),
                    FLastUpdateUserID = table.Column<Guid>(maxLength: 32, nullable: true),
                    FTitle = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_PL_ShortMsgTemplate", x => x.FID);
                });

            migrationBuilder.CreateTable(
                name: "T_PL_SiteSetting",
                columns: table => new
                {
                    FID = table.Column<Guid>(maxLength: 32, nullable: false),
                    FCode = table.Column<string>(maxLength: 20, nullable: true),
                    FContractAddress = table.Column<string>(maxLength: 200, nullable: true),
                    FContractEmail = table.Column<string>(maxLength: 50, nullable: true),
                    FContractPhone = table.Column<string>(maxLength: 50, nullable: true),
                    FContractQQ = table.Column<string>(maxLength: 50, nullable: true),
                    FCreateTime = table.Column<DateTime>(nullable: false),
                    FCreatorID = table.Column<Guid>(maxLength: 32, nullable: false),
                    FIsAdmin = table.Column<bool>(nullable: false),
                    FLanguageTypeID = table.Column<Guid>(maxLength: 32, nullable: false),
                    FLastUpdateTime = table.Column<DateTime>(nullable: true),
                    FLastUpdateUserID = table.Column<Guid>(maxLength: 32, nullable: true),
                    FLogo = table.Column<string>(maxLength: 50, nullable: true),
                    FLogoIco = table.Column<string>(maxLength: 50, nullable: true),
                    FQrCode = table.Column<string>(maxLength: 50, nullable: true),
                    FSiteRight = table.Column<string>(maxLength: 200, nullable: true),
                    FTitle = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_PL_SiteSetting", x => x.FID);
                });

            migrationBuilder.CreateTable(
                name: "T_PL_SysLog",
                columns: table => new
                {
                    FID = table.Column<Guid>(maxLength: 32, nullable: false),
                    FContent = table.Column<string>(maxLength: 1000, nullable: false),
                    FCreateTime = table.Column<DateTime>(nullable: false),
                    FCreatorID = table.Column<Guid>(maxLength: 32, nullable: false),
                    FIP = table.Column<string>(maxLength: 20, nullable: false),
                    FIsAdmin = table.Column<bool>(nullable: false),
                    FLastUpdateTime = table.Column<DateTime>(nullable: true),
                    FLastUpdateUserID = table.Column<Guid>(maxLength: 32, nullable: true),
                    FLogType = table.Column<string>(maxLength: 50, nullable: false),
                    FNum = table.Column<decimal>(nullable: false),
                    FSecurityLevel = table.Column<string>(maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_PL_SysLog", x => x.FID);
                });

            migrationBuilder.CreateTable(
                name: "T_CMS_UserMenu",
                columns: table => new
                {
                    FID = table.Column<Guid>(maxLength: 32, nullable: false),
                    FCode = table.Column<string>(maxLength: 20, nullable: false),
                    FCreateTime = table.Column<DateTime>(nullable: false),
                    FCreatorID = table.Column<Guid>(maxLength: 32, nullable: false),
                    FIcon = table.Column<string>(maxLength: 10, nullable: true),
                    FLanguageTypeID = table.Column<Guid>(maxLength: 32, nullable: false),
                    FLastUpdateTime = table.Column<DateTime>(nullable: true),
                    FLastUpdateUserID = table.Column<Guid>(maxLength: 32, nullable: true),
                    FName = table.Column<string>(maxLength: 50, nullable: false),
                    FParentId = table.Column<Guid>(maxLength: 32, nullable: false),
                    FRemarks = table.Column<string>(maxLength: 200, nullable: true),
                    FSerialNumber = table.Column<int>(nullable: false),
                    FType = table.Column<int>(nullable: false),
                    FUrl = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_CMS_UserMenu", x => x.FID);
                });

            migrationBuilder.CreateTable(
                name: "T_PL_VerifyCode",
                columns: table => new
                {
                    FID = table.Column<Guid>(maxLength: 32, nullable: false),
                    FBussinessType = table.Column<short>(nullable: false),
                    FCode = table.Column<string>(maxLength: 10, nullable: true),
                    FCreateTime = table.Column<DateTime>(nullable: false),
                    FCreatorID = table.Column<Guid>(maxLength: 32, nullable: false),
                    FLastUpdateTime = table.Column<DateTime>(nullable: true),
                    FLastUpdateUserID = table.Column<Guid>(maxLength: 32, nullable: true),
                    FPhone = table.Column<string>(maxLength: 20, nullable: true),
                    FSendTime = table.Column<DateTime>(nullable: false),
                    FValidTime = table.Column<DateTime>(nullable: true),
                    FValidTimeLen = table.Column<short>(nullable: false),
                    FVerifyFlag = table.Column<short>(nullable: false),
                    FVerifyTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_PL_VerifyCode", x => x.FID);
                });

            migrationBuilder.CreateTable(
                name: "T_AM_User",
                columns: table => new
                {
                    FID = table.Column<Guid>(maxLength: 32, nullable: false),
                    FCreateTime = table.Column<DateTime>(nullable: false),
                    FCreatorID = table.Column<Guid>(maxLength: 32, nullable: false),
                    FDepartmentId = table.Column<Guid>(maxLength: 32, nullable: false),
                    FEMail = table.Column<string>(maxLength: 50, nullable: true),
                    FIsDeleted = table.Column<bool>(nullable: false),
                    FLastLoginTime = table.Column<DateTime>(nullable: false),
                    FLastUpdateTime = table.Column<DateTime>(nullable: true),
                    FLastUpdateUserID = table.Column<Guid>(maxLength: 32, nullable: true),
                    FLoginTimes = table.Column<int>(nullable: false),
                    FMobileNumber = table.Column<string>(maxLength: 20, nullable: true),
                    FName = table.Column<string>(maxLength: 50, nullable: true),
                    FPassword = table.Column<string>(maxLength: 200, nullable: false),
                    FRemarks = table.Column<string>(maxLength: 200, nullable: true),
                    FUserName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_AM_User", x => x.FID);
                    table.ForeignKey(
                        name: "FK_T_AM_User_T_AM_Department_FDepartmentId",
                        column: x => x.FDepartmentId,
                        principalTable: "T_AM_Department",
                        principalColumn: "FID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_AM_RoleMenu",
                columns: table => new
                {
                    FRoleId = table.Column<Guid>(maxLength: 32, nullable: false),
                    FMenuId = table.Column<Guid>(maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_AM_RoleMenu", x => new { x.FRoleId, x.FMenuId });
                    table.ForeignKey(
                        name: "FK_T_AM_RoleMenu_T_AM_Menu_FMenuId",
                        column: x => x.FMenuId,
                        principalTable: "T_AM_Menu",
                        principalColumn: "FID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_T_AM_RoleMenu_T_AM_Role_FRoleId",
                        column: x => x.FRoleId,
                        principalTable: "T_AM_Role",
                        principalColumn: "FID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_AM_UserRole",
                columns: table => new
                {
                    FUserId = table.Column<Guid>(maxLength: 32, nullable: false),
                    FRoleId = table.Column<Guid>(maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_AM_UserRole", x => new { x.FUserId, x.FRoleId });
                    table.ForeignKey(
                        name: "FK_T_AM_UserRole_T_AM_Role_FRoleId",
                        column: x => x.FRoleId,
                        principalTable: "T_AM_Role",
                        principalColumn: "FID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_T_AM_UserRole_T_AM_User_FUserId",
                        column: x => x.FUserId,
                        principalTable: "T_AM_User",
                        principalColumn: "FID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_AM_RoleMenu_FMenuId",
                table: "T_AM_RoleMenu",
                column: "FMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_T_AM_User_FDepartmentId",
                table: "T_AM_User",
                column: "FDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_T_AM_UserRole_FRoleId",
                table: "T_AM_UserRole",
                column: "FRoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_CMS_Article");

            migrationBuilder.DropTable(
                name: "T_CMS_ArticleType");

            migrationBuilder.DropTable(
                name: "T_CMS_Banner");

            migrationBuilder.DropTable(
                name: "T_PST_CustomerUser");

            migrationBuilder.DropTable(
                name: "T_PL_LanguageType");

            migrationBuilder.DropTable(
                name: "T_PL_MailConfig");

            migrationBuilder.DropTable(
                name: "T_PL_MailTemplate");

            migrationBuilder.DropTable(
                name: "T_PL_PlateSetting");

            migrationBuilder.DropTable(
                name: "T_PL_PushService");

            migrationBuilder.DropTable(
                name: "T_AM_RoleMenu");

            migrationBuilder.DropTable(
                name: "T_PL_ShortMsgConfig");

            migrationBuilder.DropTable(
                name: "T_PL_ShortMsgTemplate");

            migrationBuilder.DropTable(
                name: "T_PL_SiteSetting");

            migrationBuilder.DropTable(
                name: "T_PL_SysLog");

            migrationBuilder.DropTable(
                name: "T_CMS_UserMenu");

            migrationBuilder.DropTable(
                name: "T_AM_UserRole");

            migrationBuilder.DropTable(
                name: "T_PL_VerifyCode");

            migrationBuilder.DropTable(
                name: "T_AM_Menu");

            migrationBuilder.DropTable(
                name: "T_AM_Role");

            migrationBuilder.DropTable(
                name: "T_AM_User");

            migrationBuilder.DropTable(
                name: "T_AM_Department");
        }
    }
}
