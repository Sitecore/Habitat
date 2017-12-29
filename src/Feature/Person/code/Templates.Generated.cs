namespace Sitecore.Feature.Person
{
    using global::Sitecore.Data;
    
    public partial struct Templates
    {

        public partial struct Employee
        {
            public static readonly ID ID = new ID("{745652AE-3298-48B1-9BE1-99012D91F3AC}");

            public partial struct Fields
            {
                public static readonly ID Name = new ID("{26CD59AB-3639-488F-BAFD-58D2B217755A}");
                public const string Name_FieldName = "Name";
                public static readonly ID Picture = new ID("{C9BAF3EB-8CFA-4BF6-9B19-51D38DB5FC38}");
                public const string Picture_FieldName = "Picture";
                public static readonly ID Summary = new ID("{B897023C-15D2-49F3-8974-06FA5FB7AD00}");
                public const string Summary_FieldName = "Summary";
                public static readonly ID Title = new ID("{76972FCD-4BB0-4255-864E-077745EFDF50}");
                public const string Title_FieldName = "Title";
                public static readonly ID Email = new ID("{5978B330-1D46-4065-8751-F74BF17D815E}");
                public const string Email_FieldName = "Email";
                public static readonly ID Mobile = new ID("{25B4CEE3-A61A-4DC3-BB52-775DD509DBB5}");
                public const string Mobile_FieldName = "Mobile";
                public static readonly ID Telephone = new ID("{8D0E8EE3-21C4-4AE2-A2F1-53D3F3EBE501}");
                public const string Telephone_FieldName = "Telephone";
                public static readonly ID Biography = new ID("{0CC9785E-54FE-4FAE-93E4-D0D2AE75F339}");
                public const string Biography_FieldName = "Biography";
                public static readonly ID BlogLink = new ID("{69A846D9-4C7F-435C-A8DC-87E2D7359CFA}");
                public const string BlogLink_FieldName = "BlogLink";
                public static readonly ID FacebookLink = new ID("{D31889C0-E34C-4904-A6F3-F3D92D314AA9}");
                public const string FacebookLink_FieldName = "FacebookLink";
                public static readonly ID LinkedInLink = new ID("{2B13DFAB-3450-45EF-93F4-BEAA6F544FA6}");
                public const string LinkedInLink_FieldName = "LinkedInLink";
                public static readonly ID TwitterLink = new ID("{6DE98EF7-1209-40A3-A63E-16DBEF015211}");
                public const string TwitterLink_FieldName = "TwitterLink";
            }
        }

        public partial struct Person
        {
            public static readonly ID ID = new ID("{7ACA6ECF-1A80-4E35-97F5-DBAA8E3EC617}");

            public partial struct Fields
            {
                public static readonly ID Name = new ID("{26CD59AB-3639-488F-BAFD-58D2B217755A}");
                public const string Name_FieldName = "Name";
                public static readonly ID Picture = new ID("{C9BAF3EB-8CFA-4BF6-9B19-51D38DB5FC38}");
                public const string Picture_FieldName = "Picture";
                public static readonly ID Summary = new ID("{B897023C-15D2-49F3-8974-06FA5FB7AD00}");
                public const string Summary_FieldName = "Summary";
                public static readonly ID Title = new ID("{76972FCD-4BB0-4255-864E-077745EFDF50}");
                public const string Title_FieldName = "Title";
            }
        }

        public partial struct Quote
        {
            public static readonly ID ID = new ID("{755F1188-D385-4717-8681-EF45F2258575}");

            public partial struct Fields
            {
                public static readonly ID Name = new ID("{26CD59AB-3639-488F-BAFD-58D2B217755A}");
                public const string Name_FieldName = "Name";
                public static readonly ID Picture = new ID("{C9BAF3EB-8CFA-4BF6-9B19-51D38DB5FC38}");
                public const string Picture_FieldName = "Picture";
                public static readonly ID Summary = new ID("{B897023C-15D2-49F3-8974-06FA5FB7AD00}");
                public const string Summary_FieldName = "Summary";
                public static readonly ID Title = new ID("{76972FCD-4BB0-4255-864E-077745EFDF50}");
                public const string Title_FieldName = "Title";
                public static readonly ID CiteOrigin = new ID("{BF83983A-473F-4A49-BE8E-7D563AA5687E}");
                public const string CiteOrigin_FieldName = "Cite Origin";
                public static readonly ID Quote = new ID("{0DE53078-0DA4-40CC-BBCA-63AA96A0A1EF}");
                public const string Quote_FieldName = "Quote";
            }
        }
    }
}