﻿// <auto-generated />
using System;
using Hybrid.EntityFrameworkCore.Defaults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IdentityServer4.Web.Data.Migrations.Application
{
    [DbContext(typeof(DefaultDbContext))]
    [Migration("20200212103152_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Hybrid.Authorization.EntityInfos.EntityInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("AuditEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PropertyJson")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("TypeName")
                        .IsUnique()
                        .HasName("ClassFullNameIndex");

                    b.ToTable("EntityInfo");
                });

            modelBuilder.Entity("Hybrid.Authorization.Functions.Function", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("AccessType")
                        .HasColumnType("int");

                    b.Property<string>("Action")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Area")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<bool>("AuditEntityEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("AuditOperationEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("CacheExpirationSeconds")
                        .HasColumnType("int");

                    b.Property<string>("Controller")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<bool>("IsAccessTypeChanged")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsAjax")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsCacheSliding")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsController")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsLocked")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("Area", "Controller", "Action")
                        .IsUnique()
                        .HasName("AreaControllerActionIndex");

                    b.ToTable("Function");
                });

            modelBuilder.Entity("Hybrid.Core.Systems.KeyValue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsLocked")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ValueJson")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ValueType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("KeyValue");

                    b.HasData(
                        new
                        {
                            Id = new Guid("534d7813-0eea-44cc-b88e-a9cb010c6981"),
                            IsLocked = false,
                            Key = "Site.Name",
                            ValueJson = "\"Hybrid\"",
                            ValueType = "System.String,System.Private.CoreLib"
                        },
                        new
                        {
                            Id = new Guid("977e4bba-97b2-4759-a768-a9cb010c698c"),
                            IsLocked = false,
                            Key = "Site.Description",
                            ValueJson = "\"Hybrid with .NetStandard2.0 & Angular6\"",
                            ValueType = "System.String,System.Private.CoreLib"
                        });
                });

            modelBuilder.Entity("Hybrid.Domain.Entities.Auditing.AuditEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("EntityKey")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("OperateType")
                        .HasColumnType("int");

                    b.Property<Guid>("OperationId")
                        .HasColumnType("char(36)");

                    b.Property<string>("TypeName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("OperationId");

                    b.ToTable("AuditEntity");
                });

            modelBuilder.Entity("Hybrid.Domain.Entities.Auditing.AuditOperation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Browser")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ClientIpAddress")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Elapsed")
                        .HasColumnType("int");

                    b.Property<string>("FunctionName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Message")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("OperationSystem")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("ResultType")
                        .HasColumnType("int");

                    b.Property<string>("UserAgent")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("UserId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("AuditOperation");
                });

            modelBuilder.Entity("Hybrid.Domain.Entities.Auditing.AuditProperty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AuditEntityId")
                        .HasColumnType("char(36)");

                    b.Property<string>("DataType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("DisplayName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("FieldName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("NewValue")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("OriginalValue")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("AuditEntityId");

                    b.ToTable("AuditProperty");
                });

            modelBuilder.Entity("Hybrid.Web.Identity.Entity.LoginLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Ip")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime?>("LogoutTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UserAgent")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("LoginLog");
                });

            modelBuilder.Entity("Hybrid.Web.Identity.Entity.Organization", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Remark")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Organization");
                });

            modelBuilder.Entity("Hybrid.Web.Identity.Entity.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsLocked")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsSystem")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("NormalizedName")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Remark")
                        .HasColumnType("varchar(512) CHARACTER SET utf8mb4")
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

            modelBuilder.Entity("Hybrid.Web.Identity.Entity.RoleClaim", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaim");
                });

            modelBuilder.Entity("Hybrid.Web.Identity.Entity.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("AvatarUrl")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("IdCard")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("IdCardConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsLocked")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsSystem")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NickName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("NormalizedUserName")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Remark")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("TrueName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

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
                            Gender = 0,
                            IdCardConfirmed = false,
                            IsDeleted = false,
                            IsLocked = false,
                            IsSystem = true,
                            LockoutEnabled = true,
                            NickName = "SuperAdmin",
                            NormalizedEmail = "ADMIN@EXAMPLE.COM",
                            NormalizedUserName = "ADMIN",
                            PasswordHash = "AQAAAAEAACcQAAAAEB6lgMDV9JoidhR4cfIK+bKOQfo9eE6M02N68wV0KxCbx+c5gxkBrZWOp0FwI5Id8g==",
                            PhoneNumber = "18100000000",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "RRYXXETXCDKPXE6QPNDGLMCYNBA2ZF4P",
                            TwoFactorEnabled = false,
                            UserName = "Admin"
                        });
                });

            modelBuilder.Entity("Hybrid.Web.Identity.Entity.UserClaim", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("ClaimType")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaim");
                });

            modelBuilder.Entity("Hybrid.Web.Identity.Entity.UserDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("RegisterIp")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserDetail");
                });

            modelBuilder.Entity("Hybrid.Web.Identity.Entity.UserLogin", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Avatar")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("LoginProvider", "ProviderKey")
                        .IsUnique()
                        .HasName("UserLoginIndex");

                    b.ToTable("UserLogin");
                });

            modelBuilder.Entity("Hybrid.Web.Identity.Entity.UserRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsLocked")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId", "RoleId", "IsDeleted")
                        .IsUnique()
                        .HasName("UserRoleIndex");

                    b.ToTable("UserRole");

                    b.HasData(
                        new
                        {
                            Id = new Guid("66c1c6ab-5fa1-45f6-9704-509df0c03f28"),
                            CreatedTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            IsLocked = false,
                            RoleId = new Guid("4f10b9ba-2391-4eb2-a378-aaf3012fb2d3"),
                            UserId = new Guid("8d86feea-83d5-4a0c-9733-305ac6cfe58d")
                        });
                });

            modelBuilder.Entity("Hybrid.Web.Identity.Entity.UserToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("UserId", "LoginProvider", "Name")
                        .IsUnique()
                        .HasName("UserTokenIndex");

                    b.ToTable("UserToken");
                });

            modelBuilder.Entity("Hybrid.Web.Security.Entities.EntityRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("EntityId")
                        .HasColumnType("char(36)");

                    b.Property<string>("FilterGroupJson")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("IsLocked")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Operation")
                        .HasColumnType("int");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("EntityId", "RoleId", "Operation")
                        .IsUnique()
                        .HasName("EntityRoleIndex");

                    b.ToTable("EntityRole");
                });

            modelBuilder.Entity("Hybrid.Web.Security.Entities.EntityUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("EntityId")
                        .HasColumnType("char(36)");

                    b.Property<string>("FilterGroupJson")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("IsLocked")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("EntityId", "UserId")
                        .HasName("EntityUserIndex");

                    b.ToTable("EntityUser");
                });

            modelBuilder.Entity("Hybrid.Web.Security.Entities.Module", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("OrderCode")
                        .HasColumnType("int");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Remark")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("TreePathString")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Module");

                    b.HasData(
                        new
                        {
                            Id = new Guid("eb972ec2-e163-45e8-b314-aaef01716b06"),
                            Code = "Root",
                            Name = "根节点",
                            OrderCode = 1,
                            Remark = "系统根节点",
                            TreePathString = "$eb972ec2-e163-45e8-b314-aaef01716b06$"
                        });
                });

            modelBuilder.Entity("Hybrid.Web.Security.Entities.ModuleFunction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("FunctionId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ModuleId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("FunctionId");

                    b.HasIndex("ModuleId", "FunctionId")
                        .IsUnique()
                        .HasName("ModuleFunctionIndex");

                    b.ToTable("ModuleFunction");
                });

            modelBuilder.Entity("Hybrid.Web.Security.Entities.ModuleRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ModuleId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("ModuleId", "RoleId")
                        .IsUnique()
                        .HasName("ModuleRoleIndex");

                    b.ToTable("ModuleRole");
                });

            modelBuilder.Entity("Hybrid.Web.Security.Entities.ModuleUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("Disabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("ModuleId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("ModuleId", "UserId")
                        .IsUnique()
                        .HasName("ModuleUserIndex");

                    b.ToTable("ModuleUser");
                });

            modelBuilder.Entity("Hybrid.Domain.Entities.Auditing.AuditEntity", b =>
                {
                    b.HasOne("Hybrid.Domain.Entities.Auditing.AuditOperation", "Operation")
                        .WithMany("AuditEntities")
                        .HasForeignKey("OperationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Hybrid.Domain.Entities.Auditing.AuditProperty", b =>
                {
                    b.HasOne("Hybrid.Domain.Entities.Auditing.AuditEntity", "AuditEntity")
                        .WithMany("Properties")
                        .HasForeignKey("AuditEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Hybrid.Web.Identity.Entity.LoginLog", b =>
                {
                    b.HasOne("Hybrid.Web.Identity.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Hybrid.Web.Identity.Entity.Organization", b =>
                {
                    b.HasOne("Hybrid.Web.Identity.Entity.Organization", null)
                        .WithMany()
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("Hybrid.Web.Identity.Entity.RoleClaim", b =>
                {
                    b.HasOne("Hybrid.Web.Identity.Entity.Role", "Role")
                        .WithMany("RoleClaims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Hybrid.Web.Identity.Entity.UserClaim", b =>
                {
                    b.HasOne("Hybrid.Web.Identity.Entity.User", "User")
                        .WithMany("UserClaims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Hybrid.Web.Identity.Entity.UserDetail", b =>
                {
                    b.HasOne("Hybrid.Web.Identity.Entity.User", "User")
                        .WithOne("UserDetail")
                        .HasForeignKey("Hybrid.Web.Identity.Entity.UserDetail", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Hybrid.Web.Identity.Entity.UserLogin", b =>
                {
                    b.HasOne("Hybrid.Web.Identity.Entity.User", "User")
                        .WithMany("UserLogins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Hybrid.Web.Identity.Entity.UserRole", b =>
                {
                    b.HasOne("Hybrid.Web.Identity.Entity.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hybrid.Web.Identity.Entity.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Hybrid.Web.Identity.Entity.UserToken", b =>
                {
                    b.HasOne("Hybrid.Web.Identity.Entity.User", "User")
                        .WithMany("UserTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Hybrid.Web.Security.Entities.EntityRole", b =>
                {
                    b.HasOne("Hybrid.Authorization.EntityInfos.EntityInfo", "EntityInfo")
                        .WithMany()
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hybrid.Web.Identity.Entity.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Hybrid.Web.Security.Entities.EntityUser", b =>
                {
                    b.HasOne("Hybrid.Authorization.EntityInfos.EntityInfo", "EntityInfo")
                        .WithMany()
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hybrid.Web.Identity.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Hybrid.Web.Security.Entities.Module", b =>
                {
                    b.HasOne("Hybrid.Web.Security.Entities.Module", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("Hybrid.Web.Security.Entities.ModuleFunction", b =>
                {
                    b.HasOne("Hybrid.Authorization.Functions.Function", "Function")
                        .WithMany()
                        .HasForeignKey("FunctionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hybrid.Web.Security.Entities.Module", "Module")
                        .WithMany()
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Hybrid.Web.Security.Entities.ModuleRole", b =>
                {
                    b.HasOne("Hybrid.Web.Security.Entities.Module", "Module")
                        .WithMany()
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hybrid.Web.Identity.Entity.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Hybrid.Web.Security.Entities.ModuleUser", b =>
                {
                    b.HasOne("Hybrid.Web.Security.Entities.Module", "Module")
                        .WithMany()
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hybrid.Web.Identity.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
