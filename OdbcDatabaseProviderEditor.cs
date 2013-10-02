using Inedo.BuildMaster.Extensibility.Providers;
using Inedo.BuildMaster.Web.Controls;
using Inedo.BuildMaster.Web.Controls.Extensions;
using Inedo.Web.Controls;

namespace Inedo.BuildMasterExtensions.Odbc
{
    /// <summary>
    /// Custom editor for the ODBC database provider.
    /// </summary>
    internal sealed class OdbcDatabaseProviderEditor : ProviderEditorBase
    {
        private ValidatingTextBox txtConnectionString;

        public override void BindToForm(ProviderBase extension)
        {
            EnsureChildControls();

            var mysql = (OdbcDatabaseProvider)extension;
            txtConnectionString.Text = mysql.ConnectionString;
        }

        public override ProviderBase CreateFromForm()
        {
            EnsureChildControls();

            return new OdbcDatabaseProvider
            {
                ConnectionString = txtConnectionString.Text
            };
        }

        protected override void CreateChildControls()
        {
            this.txtConnectionString = new ValidatingTextBox
            {
                Width = 300,
                Required = true
            };

            this.Controls.Add(
                new FormFieldGroup(
                    "Connection String",
                    "The ODBC connection string. There are several formats for this, but two common ones are:<br /><br />"
                    + "<em>Driver={My Driver Name}; some_attribute=some_value;</em><br /><br />"
                    + "<em>DSN=dsn_name</em>",
                    false,
                    new StandardFormField(string.Empty, txtConnectionString)
                )
            );

            base.CreateChildControls();
        }
    }
}
