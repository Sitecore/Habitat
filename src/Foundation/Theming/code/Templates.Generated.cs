namespace Sitecore.Foundation.Theming
{
    using global::Sitecore.Data;
    
    public partial struct Templates
    {

        public partial struct ParametersTemplateFixedHeight
        {
            public static readonly ID ID = new ID("{329421CF-B99C-4E7A-AF71-F7FA7DC42BF1}");

            public partial struct Fields
            {
                public static readonly ID FixedHeight = new ID("{B8965F9D-DDB4-4B8F-BF40-AFCF3C9AE09F}");
                public const string FixedHeight_FieldName = "Fixed height";
            }
        }

        public partial struct ParametersTemplateHasBackground
        {
            public static readonly ID ID = new ID("{A2A233A1-6701-48A9-B5F8-EFEAB74B655F}");

            public partial struct Fields
            {
                public static readonly ID Background = new ID("{32439F83-C2FC-46E8-8981-5D1CDF1B2742}");
                public const string Background_FieldName = "Background";
            }
        }

        public partial struct ParametersTemplateHasContainer
        {
            public static readonly ID ID = new ID("{29299E73-6841-44C1-A65F-0889011E2EEC}");

            public partial struct Fields
            {
                public static readonly ID ContainerIsFluid = new ID("{8F49D801-02B0-4DB7-90DB-5742D1662CF1}");
                public const string ContainerIsFluid_FieldName = "ContainerIsFluid";
            }
        }

        public partial struct ParametersTemplateHasContainerWithBackground
        {
            public static readonly ID ID = new ID("{3CA3A190-897A-4EC9-8EB5-9DE2C3636569}");

            public partial struct Fields
            {
                public static readonly ID Background = new ID("{32439F83-C2FC-46E8-8981-5D1CDF1B2742}");
                public const string Background_FieldName = "Background";
                public static readonly ID ContainerIsFluid = new ID("{8F49D801-02B0-4DB7-90DB-5742D1662CF1}");
                public const string ContainerIsFluid_FieldName = "ContainerIsFluid";
            }
        }

        public partial struct Style
        {
            public static readonly ID ID = new ID("{C2AC5C42-A05C-4F51-854E-730C9BCA06D1}");

            public partial struct Fields
            {
                public static readonly ID Class = new ID("{CF1E34B0-27E7-4861-BECD-C0BC58295F77}");
                public const string Class_FieldName = "Class";
            }
        }
    }
}