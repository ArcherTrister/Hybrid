using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.

[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// https://docs.microsoft.com/zh-cn/dotnet/standard/assembly/create-signed-friend
[assembly: InternalsVisibleTo("Hybrid.AspNetCore, PublicKey=002400000480000094000000060200000024000052534131000400000100010081c10187b8cd1e439cc1cd331914c8dcd42351b3ac7fff5c6b97fc51a76529bfbd75bbbeabbf8be72fbea070e2a65f0094317aed092a6eb589b82a1d30e95afc1a2c71a905d9a33ef5b6faed040a2b376c1d44997ec04ab887ecf7ae73ed188be9c43b0189f9bcf04fd4fd249ee35f0fd26d3ba29d36b8d454cea5b9ff7bfacb")]
[assembly: InternalsVisibleTo("Hybrid.AspNetCore.Diagnostics, PublicKey=002400000480000094000000060200000024000052534131000400000100010081c10187b8cd1e439cc1cd331914c8dcd42351b3ac7fff5c6b97fc51a76529bfbd75bbbeabbf8be72fbea070e2a65f0094317aed092a6eb589b82a1d30e95afc1a2c71a905d9a33ef5b6faed040a2b376c1d44997ec04ab887ecf7ae73ed188be9c43b0189f9bcf04fd4fd249ee35f0fd26d3ba29d36b8d454cea5b9ff7bfacb")]
[assembly: InternalsVisibleTo("Hybrid.AutoMapper, PublicKey=002400000480000094000000060200000024000052534131000400000100010081c10187b8cd1e439cc1cd331914c8dcd42351b3ac7fff5c6b97fc51a76529bfbd75bbbeabbf8be72fbea070e2a65f0094317aed092a6eb589b82a1d30e95afc1a2c71a905d9a33ef5b6faed040a2b376c1d44997ec04ab887ecf7ae73ed188be9c43b0189f9bcf04fd4fd249ee35f0fd26d3ba29d36b8d454cea5b9ff7bfacb")]
[assembly: InternalsVisibleTo("Hybrid.EntityFrameworkCore, PublicKey=002400000480000094000000060200000024000052534131000400000100010081c10187b8cd1e439cc1cd331914c8dcd42351b3ac7fff5c6b97fc51a76529bfbd75bbbeabbf8be72fbea070e2a65f0094317aed092a6eb589b82a1d30e95afc1a2c71a905d9a33ef5b6faed040a2b376c1d44997ec04ab887ecf7ae73ed188be9c43b0189f9bcf04fd4fd249ee35f0fd26d3ba29d36b8d454cea5b9ff7bfacb")]
[assembly: InternalsVisibleTo("Hybrid.EntityFrameworkCore.MySql, PublicKey=002400000480000094000000060200000024000052534131000400000100010081c10187b8cd1e439cc1cd331914c8dcd42351b3ac7fff5c6b97fc51a76529bfbd75bbbeabbf8be72fbea070e2a65f0094317aed092a6eb589b82a1d30e95afc1a2c71a905d9a33ef5b6faed040a2b376c1d44997ec04ab887ecf7ae73ed188be9c43b0189f9bcf04fd4fd249ee35f0fd26d3ba29d36b8d454cea5b9ff7bfacb")]
[assembly: InternalsVisibleTo("Hybrid.EntityFrameworkCore.Sqlite, PublicKey=002400000480000094000000060200000024000052534131000400000100010081c10187b8cd1e439cc1cd331914c8dcd42351b3ac7fff5c6b97fc51a76529bfbd75bbbeabbf8be72fbea070e2a65f0094317aed092a6eb589b82a1d30e95afc1a2c71a905d9a33ef5b6faed040a2b376c1d44997ec04ab887ecf7ae73ed188be9c43b0189f9bcf04fd4fd249ee35f0fd26d3ba29d36b8d454cea5b9ff7bfacb")]
[assembly: InternalsVisibleTo("Hybrid.EntityFrameworkCore.SqlServer, PublicKey=002400000480000094000000060200000024000052534131000400000100010081c10187b8cd1e439cc1cd331914c8dcd42351b3ac7fff5c6b97fc51a76529bfbd75bbbeabbf8be72fbea070e2a65f0094317aed092a6eb589b82a1d30e95afc1a2c71a905d9a33ef5b6faed040a2b376c1d44997ec04ab887ecf7ae73ed188be9c43b0189f9bcf04fd4fd249ee35f0fd26d3ba29d36b8d454cea5b9ff7bfacb")]
[assembly: InternalsVisibleTo("Hybrid.MailKit, PublicKey=002400000480000094000000060200000024000052534131000400000100010081c10187b8cd1e439cc1cd331914c8dcd42351b3ac7fff5c6b97fc51a76529bfbd75bbbeabbf8be72fbea070e2a65f0094317aed092a6eb589b82a1d30e95afc1a2c71a905d9a33ef5b6faed040a2b376c1d44997ec04ab887ecf7ae73ed188be9c43b0189f9bcf04fd4fd249ee35f0fd26d3ba29d36b8d454cea5b9ff7bfacb")]
[assembly: InternalsVisibleTo("Hybrid.NLog, PublicKey=002400000480000094000000060200000024000052534131000400000100010081c10187b8cd1e439cc1cd331914c8dcd42351b3ac7fff5c6b97fc51a76529bfbd75bbbeabbf8be72fbea070e2a65f0094317aed092a6eb589b82a1d30e95afc1a2c71a905d9a33ef5b6faed040a2b376c1d44997ec04ab887ecf7ae73ed188be9c43b0189f9bcf04fd4fd249ee35f0fd26d3ba29d36b8d454cea5b9ff7bfacb")]
[assembly: InternalsVisibleTo("Hybrid.Quartz, PublicKey=002400000480000094000000060200000024000052534131000400000100010081c10187b8cd1e439cc1cd331914c8dcd42351b3ac7fff5c6b97fc51a76529bfbd75bbbeabbf8be72fbea070e2a65f0094317aed092a6eb589b82a1d30e95afc1a2c71a905d9a33ef5b6faed040a2b376c1d44997ec04ab887ecf7ae73ed188be9c43b0189f9bcf04fd4fd249ee35f0fd26d3ba29d36b8d454cea5b9ff7bfacb")]
[assembly: InternalsVisibleTo("Hybrid.Redis, PublicKey=002400000480000094000000060200000024000052534131000400000100010081c10187b8cd1e439cc1cd331914c8dcd42351b3ac7fff5c6b97fc51a76529bfbd75bbbeabbf8be72fbea070e2a65f0094317aed092a6eb589b82a1d30e95afc1a2c71a905d9a33ef5b6faed040a2b376c1d44997ec04ab887ecf7ae73ed188be9c43b0189f9bcf04fd4fd249ee35f0fd26d3ba29d36b8d454cea5b9ff7bfacb")]
[assembly: InternalsVisibleTo("Hybrid.Zero, PublicKey=002400000480000094000000060200000024000052534131000400000100010081c10187b8cd1e439cc1cd331914c8dcd42351b3ac7fff5c6b97fc51a76529bfbd75bbbeabbf8be72fbea070e2a65f0094317aed092a6eb589b82a1d30e95afc1a2c71a905d9a33ef5b6faed040a2b376c1d44997ec04ab887ecf7ae73ed188be9c43b0189f9bcf04fd4fd249ee35f0fd26d3ba29d36b8d454cea5b9ff7bfacb")]
[assembly: InternalsVisibleTo("Hybrid.Zero.Identity, PublicKey=002400000480000094000000060200000024000052534131000400000100010081c10187b8cd1e439cc1cd331914c8dcd42351b3ac7fff5c6b97fc51a76529bfbd75bbbeabbf8be72fbea070e2a65f0094317aed092a6eb589b82a1d30e95afc1a2c71a905d9a33ef5b6faed040a2b376c1d44997ec04ab887ecf7ae73ed188be9c43b0189f9bcf04fd4fd249ee35f0fd26d3ba29d36b8d454cea5b9ff7bfacb")]
[assembly: InternalsVisibleTo("Hybrid.Zero.IdentityServer4, PublicKey=002400000480000094000000060200000024000052534131000400000100010081c10187b8cd1e439cc1cd331914c8dcd42351b3ac7fff5c6b97fc51a76529bfbd75bbbeabbf8be72fbea070e2a65f0094317aed092a6eb589b82a1d30e95afc1a2c71a905d9a33ef5b6faed040a2b376c1d44997ec04ab887ecf7ae73ed188be9c43b0189f9bcf04fd4fd249ee35f0fd26d3ba29d36b8d454cea5b9ff7bfacb")]
//[assembly: InternalsVisibleTo("Hybrid.Zero.UI, PublicKey=002400000480000094000000060200000024000052534131000400000100010081c10187b8cd1e439cc1cd331914c8dcd42351b3ac7fff5c6b97fc51a76529bfbd75bbbeabbf8be72fbea070e2a65f0094317aed092a6eb589b82a1d30e95afc1a2c71a905d9a33ef5b6faed040a2b376c1d44997ec04ab887ecf7ae73ed188be9c43b0189f9bcf04fd4fd249ee35f0fd26d3ba29d36b8d454cea5b9ff7bfacb")]
// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("fc1c99c5-557f-4046-987a-cbbebf047672")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// [assembly: AssemblyVersion("1.0.0.0")]
// [assembly: AssemblyFileVersion("1.0.0.0")]