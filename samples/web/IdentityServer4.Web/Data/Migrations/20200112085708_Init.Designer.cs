﻿// <auto-generated />
using System;
using ESoftor.EntityFrameworkCore.Defaults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IdentityServer4.Web.Data.Migrations
{
    [DbContext(typeof(DefaultDbContext))]
    [Migration("20200112085708_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ESoftor.Core.EntityInfos.EntityInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AuditEnabled");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("PropertyJson")
                        .IsRequired();

                    b.Property<string>("TypeName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("TypeName")
                        .IsUnique()
                        .HasName("ClassFullNameIndex");

                    b.ToTable("EntityInfo");
                });

            modelBuilder.Entity("ESoftor.Core.Functions.Function", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessType");

                    b.Property<string>("Action");

                    b.Property<string>("Area");

                    b.Property<bool>("AuditEntityEnabled");

                    b.Property<bool>("AuditOperationEnabled");

                    b.Property<int>("CacheExpirationSeconds");

                    b.Property<string>("Controller");

                    b.Property<bool>("IsAccessTypeChanged");

                    b.Property<bool>("IsAjax");

                    b.Property<bool>("IsCacheSliding");

                    b.Property<bool>("IsController");

                    b.Property<bool>("IsLocked");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("Area", "Controller", "Action")
                        .IsUnique()
                        .HasName("AreaControllerActionIndex")
                        .HasFilter("[Area] IS NOT NULL AND [Controller] IS NOT NULL AND [Action] IS NOT NULL");

                    b.ToTable("Function");
                });

            modelBuilder.Entity("ESoftor.Core.Systems.KeyValue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsLocked");

                    b.Property<string>("Key")
                        .IsRequired();

                    b.Property<string>("ValueJson");

                    b.Property<string>("ValueType");

                    b.HasKey("Id");

                    b.ToTable("KeyValue");

                    b.HasData(
                        new
                        {
                            Id = new Guid("534d7813-0eea-44cc-b88e-a9cb010c6981"),
                            IsLocked = false,
                            Key = "Site.Name",
                            ValueJson = "\"ESoftor\"",
                            ValueType = "System.String,System.Private.CoreLib"
                        },
                        new
                        {
                            Id = new Guid("977e4bba-97b2-4759-a768-a9cb010c698c"),
                            IsLocked = false,
                            Key = "Site.Description",
                            ValueJson = "\"ESoftor with .NetStandard2.0 & Angular6\"",
                            ValueType = "System.String,System.Private.CoreLib"
                        });
                });

            modelBuilder.Entity("ESoftor.Domain.Entities.Auditing.AuditEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EntityKey");

                    b.Property<string>("Name");

                    b.Property<int>("OperateType");

                    b.Property<Guid>("OperationId");

                    b.Property<string>("TypeName");

                    b.HasKey("Id");

                    b.HasIndex("OperationId");

                    b.ToTable("AuditEntity");
                });

            modelBuilder.Entity("ESoftor.Domain.Entities.Auditing.AuditOperation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Browser");

                    b.Property<string>("ClientIpAddress");

                    b.Property<DateTime>("CreatedTime");

                    b.Property<int>("Elapsed");

                    b.Property<string>("FunctionName");

                    b.Property<string>("Message");

                    b.Property<string>("OperationSystem");

                    b.Property<int>("ResultType");

                    b.Property<string>("UserAgent");

                    b.Property<string>("UserId");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("AuditOperation");
                });

            modelBuilder.Entity("ESoftor.Domain.Entities.Auditing.AuditProperty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AuditEntityId");

                    b.Property<string>("DataType");

                    b.Property<string>("DisplayName");

                    b.Property<string>("FieldName");

                    b.Property<string>("NewValue");

                    b.Property<string>("OriginalValue");

                    b.HasKey("Id");

                    b.HasIndex("AuditEntityId");

                    b.ToTable("AuditProperty");
                });

            modelBuilder.Entity("ESoftor.Web.Identity.Entity.LoginLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedTime");

                    b.Property<string>("Ip");

                    b.Property<DateTime?>("LogoutTime");

                    b.Property<string>("UserAgent");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("LoginLog");
                });

            modelBuilder.Entity("ESoftor.Web.Identity.Entity.Organization", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<Guid?>("ParentId");

                    b.Property<string>("Remark");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Organization");
                });

            modelBuilder.Entity("ESoftor.Web.Identity.Entity.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreatedTime");

                    b.Property<bool>("IsAdmin");

                    b.Property<bool>("IsDefault");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsLocked");

                    b.Property<bool>("IsSystem");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("NormalizedName")
                        .IsRequired();

                    b.Property<string>("Remark")
                        .HasMaxLength(512);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName", "IsDeleted")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            Id = new Guid("4f10b9ba-2391-4eb2-a378-aaf3012fb2d3"),
                            ConcurrencyStamp = "97313840-7874-47e5-81f2-565613c8cdcc",
                            CreatedTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsAdmin = true,
                            IsDefault = false,
                            IsDeleted = false,
                            IsLocked = false,
                            IsSystem = true,
                            Name = "系统管理员",
                            NormalizedName = "系统管理员",
                            Remark = "系统最高权限管理角色"
                        });
                });

            modelBuilder.Entity("ESoftor.Web.Identity.Entity.RoleClaim", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaim");
                });

            modelBuilder.Entity("ESoftor.Web.Identity.Entity.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreatedTime");

                    b.Property<string>("Email");

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("HeadImg");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsLocked");

                    b.Property<bool>("IsSystem");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NickName");

                    b.Property<string>("NormalizedEmail");

                    b.Property<string>("NormalizedUserName")
                        .IsRequired();

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("Remark");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail", "IsDeleted")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName", "IsDeleted")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = new Guid("8d86feea-83d5-4a0c-9733-305ac6cfe58d"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "e50ea89e-c966-4ade-8fe4-6fe94de83777",
                            CreatedTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "Admin@example.com",
                            EmailConfirmed = true,
                            IsDeleted = false,
                            IsLocked = false,
                            IsSystem = true,
                            LockoutEnabled = true,
                            NickName = "SuperAdmin",
                            NormalizedEmail = "ADMIN@EXAMPLE.COM",
                            NormalizedUserName = "ADMIN",
                            PasswordHash = "AQAAAAEAACcQAAAAEB6lgMDV9JoidhR4cfIK+bKOQfo9eE6M02N68wV0KxCbx+c5gxkBrZWOp0FwI5Id8g==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "RRYXXETXCDKPXE6QPNDGLMCYNBA2ZF4P",
                            TwoFactorEnabled = false,
                            UserName = "Admin"
                        });
                });

            modelBuilder.Entity("ESoftor.Web.Identity.Entity.UserClaim", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType")
                        .IsRequired();

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaim");
                });

            modelBuilder.Entity("ESoftor.Web.Identity.Entity.UserDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("RegisterIp");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserDetail");
                });

            modelBuilder.Entity("ESoftor.Web.Identity.Entity.UserLogin", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Avatar");

                    b.Property<DateTime>("CreatedTime");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("ProviderKey");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("LoginProvider", "ProviderKey")
                        .IsUnique()
                        .HasName("UserLoginIndex")
                        .HasFilter("[LoginProvider] IS NOT NULL AND [ProviderKey] IS NOT NULL");

                    b.ToTable("UserLogin");
                });

            modelBuilder.Entity("ESoftor.Web.Identity.Entity.UserRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsLocked");

                    b.Property<Guid>("RoleId");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId", "RoleId", "IsDeleted")
                        .IsUnique()
                        .HasName("UserRoleIndex");

                    b.ToTable("UserRole");

                    b.HasData(
                        new
                        {
                            Id = new Guid("479415b8-f7ca-41fe-9bc9-135655a08ece"),
                            CreatedTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            IsLocked = false,
                            RoleId = new Guid("4f10b9ba-2391-4eb2-a378-aaf3012fb2d3"),
                            UserId = new Guid("8d86feea-83d5-4a0c-9733-305ac6cfe58d")
                        });
                });

            modelBuilder.Entity("ESoftor.Web.Identity.Entity.UserToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<Guid>("UserId");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("UserId", "LoginProvider", "Name")
                        .IsUnique()
                        .HasName("UserTokenIndex")
                        .HasFilter("[LoginProvider] IS NOT NULL AND [Name] IS NOT NULL");

                    b.ToTable("UserToken");
                });

            modelBuilder.Entity("ESoftor.Web.Security.Entities.EntityRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedTime");

                    b.Property<Guid>("EntityId");

                    b.Property<string>("FilterGroupJson");

                    b.Property<bool>("IsLocked");

                    b.Property<int>("Operation");

                    b.Property<Guid>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("EntityId", "RoleId", "Operation")
                        .IsUnique()
                        .HasName("EntityRoleIndex");

                    b.ToTable("EntityRole");
                });

            modelBuilder.Entity("ESoftor.Web.Security.Entities.EntityUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedTime");

                    b.Property<Guid>("EntityId");

                    b.Property<string>("FilterGroupJson");

                    b.Property<bool>("IsLocked");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("EntityId", "UserId")
                        .HasName("EntityUserIndex");

                    b.ToTable("EntityUser");
                });

            modelBuilder.Entity("ESoftor.Web.Security.Entities.Module", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<double>("OrderCode");

                    b.Property<Guid?>("ParentId");

                    b.Property<string>("Remark");

                    b.Property<string>("TreePathString");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Module");

                    b.HasData(
                        new
                        {
                            Id = new Guid("eb972ec2-e163-45e8-b314-aaef01716b06"),
                            Code = "Root",
                            Name = "根节点",
                            OrderCode = 1.0,
                            Remark = "系统根节点",
                            TreePathString = "$eb972ec2-e163-45e8-b314-aaef01716b06$"
                        });
                });

            modelBuilder.Entity("ESoftor.Web.Security.Entities.ModuleFunction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("FunctionId");

                    b.Property<Guid>("ModuleId");

                    b.HasKey("Id");

                    b.HasIndex("FunctionId");

                    b.HasIndex("ModuleId", "FunctionId")
                        .IsUnique()
                        .HasName("ModuleFunctionIndex");

                    b.ToTable("ModuleFunction");
                });

            modelBuilder.Entity("ESoftor.Web.Security.Entities.ModuleRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ModuleId");

                    b.Property<Guid>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("ModuleId", "RoleId")
                        .IsUnique()
                        .HasName("ModuleRoleIndex");

                    b.ToTable("ModuleRole");
                });

            modelBuilder.Entity("ESoftor.Web.Security.Entities.ModuleUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Disabled");

                    b.Property<Guid>("ModuleId");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("ModuleId", "UserId")
                        .IsUnique()
                        .HasName("ModuleUserIndex");

                    b.ToTable("ModuleUser");
                });

            modelBuilder.Entity("ESoftor.Domain.Entities.Auditing.AuditEntity", b =>
                {
                    b.HasOne("ESoftor.Domain.Entities.Auditing.AuditOperation", "Operation")
                        .WithMany("AuditEntities")
                        .HasForeignKey("OperationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ESoftor.Domain.Entities.Auditing.AuditProperty", b =>
                {
                    b.HasOne("ESoftor.Domain.Entities.Auditing.AuditEntity", "AuditEntity")
                        .WithMany("Properties")
                        .HasForeignKey("AuditEntityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ESoftor.Web.Identity.Entity.LoginLog", b =>
                {
                    b.HasOne("ESoftor.Web.Identity.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ESoftor.Web.Identity.Entity.Organization", b =>
                {
                    b.HasOne("ESoftor.Web.Identity.Entity.Organization")
                        .WithMany()
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("ESoftor.Web.Identity.Entity.RoleClaim", b =>
                {
                    b.HasOne("ESoftor.Web.Identity.Entity.Role", "Role")
                        .WithMany("RoleClaims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ESoftor.Web.Identity.Entity.UserClaim", b =>
                {
                    b.HasOne("ESoftor.Web.Identity.Entity.User", "User")
                        .WithMany("UserClaims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ESoftor.Web.Identity.Entity.UserDetail", b =>
                {
                    b.HasOne("ESoftor.Web.Identity.Entity.User", "User")
                        .WithOne("UserDetail")
                        .HasForeignKey("ESoftor.Web.Identity.Entity.UserDetail", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ESoftor.Web.Identity.Entity.UserLogin", b =>
                {
                    b.HasOne("ESoftor.Web.Identity.Entity.User", "User")
                        .WithMany("UserLogins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ESoftor.Web.Identity.Entity.UserRole", b =>
                {
                    b.HasOne("ESoftor.Web.Identity.Entity.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ESoftor.Web.Identity.Entity.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ESoftor.Web.Identity.Entity.UserToken", b =>
                {
                    b.HasOne("ESoftor.Web.Identity.Entity.User", "User")
                        .WithMany("UserTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ESoftor.Web.Security.Entities.EntityRole", b =>
                {
                    b.HasOne("ESoftor.Core.EntityInfos.EntityInfo", "EntityInfo")
                        .WithMany()
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ESoftor.Web.Identity.Entity.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ESoftor.Web.Security.Entities.EntityUser", b =>
                {
                    b.HasOne("ESoftor.Core.EntityInfos.EntityInfo", "EntityInfo")
                        .WithMany()
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ESoftor.Web.Identity.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ESoftor.Web.Security.Entities.Module", b =>
                {
                    b.HasOne("ESoftor.Web.Security.Entities.Module", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("ESoftor.Web.Security.Entities.ModuleFunction", b =>
                {
                    b.HasOne("ESoftor.Core.Functions.Function", "Function")
                        .WithMany()
                        .HasForeignKey("FunctionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ESoftor.Web.Security.Entities.Module", "Module")
                        .WithMany()
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ESoftor.Web.Security.Entities.ModuleRole", b =>
                {
                    b.HasOne("ESoftor.Web.Security.Entities.Module", "Module")
                        .WithMany()
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ESoftor.Web.Identity.Entity.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ESoftor.Web.Security.Entities.ModuleUser", b =>
                {
                    b.HasOne("ESoftor.Web.Security.Entities.Module", "Module")
                        .WithMany()
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ESoftor.Web.Identity.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
