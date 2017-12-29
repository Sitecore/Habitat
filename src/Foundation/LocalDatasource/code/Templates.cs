namespace Sitecore.Foundation.LocalDatasource
{
    using global::Sitecore.Data;

    public partial struct Templates
    {
        public struct Index
        {
            public struct Fields
            {
                public static readonly string LocalDatasourceContent_IndexFieldName = "local_datasource_content";
            }
        }

        public partial struct RenderingOptions
        {
            public static ID ID = new ID("{D1592226-3898-4CE2-B190-090FD5F84A4C}");

            public partial struct Fields
            {
                public static readonly ID SupportsLocalDatasource = new ID("{1C307764-806C-42F0-B7CE-FC173AC8372B}");
            }
        }
    }
}