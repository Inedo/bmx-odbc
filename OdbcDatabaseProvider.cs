using System.Data.Odbc;
using Inedo.BuildMaster.Extensibility.Providers;
using Inedo.BuildMaster.Extensibility.Providers.Database;
using Inedo.BuildMaster.Web;

namespace Inedo.BuildMasterExtensions.Odbc
{
    [ProviderProperties(
        "ODBC (Limited)",
        "Supports ODBC databases, but does not provide change script functionality; a more specific provider should be chosen when possible.")]
    [CustomEditor(typeof(OdbcDatabaseProviderEditor))]
    public sealed class OdbcDatabaseProvider : DatabaseProviderBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OdbcDatabaseProvider"/> class.
        /// </summary>
        public OdbcDatabaseProvider()
        {
        }

        public override bool IsAvailable()
        {
            return true;
        }
        public override void ValidateConnection()
        {
            using (var conn = new OdbcConnection(this.ConnectionString))
            {
                conn.Open();
                conn.Close();
            }
        }
        public override void ExecuteQueries(string[] queries)
        {
            if (queries == null || queries.Length == 0)
                return;

            using (var conn = new OdbcConnection(this.ConnectionString))
            {
                conn.Open();

                using (var cmd = new OdbcCommand(string.Empty, conn))
                {
                    foreach (var query in queries)
                    {
                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        public override void ExecuteQuery(string query)
        {
            if (string.IsNullOrEmpty(query))
                return;

            using (var conn = new OdbcConnection(this.ConnectionString))
            {
                conn.Open();

                using (var cmd = new OdbcCommand(query, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public override string ToString()
        {
            var csb = new OdbcConnectionStringBuilder(this.ConnectionString);
            return string.Format("ODBC Connection ({0})", csb.Driver);
        }
    }
}
