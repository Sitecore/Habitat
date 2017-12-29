namespace Sitecore.Feature.Faq
{
    using global::Sitecore.Data;
    
    public partial struct Templates
    {

        public partial struct FAQGroup
        {
            public static readonly ID ID = new ID("{3AF7DB6C-A602-4ABC-8D63-19E2D2C6726B}");

            public partial struct Fields
            {
                public static readonly ID GroupMember = new ID("{631DA648-E2A5-4E3B-9733-C9C066C41EAE}");
                public const string GroupMember_FieldName = "Group Member";
            }
        }

        public partial struct FAQ
        {
            public static readonly ID ID = new ID("{9544F0B3-FD5E-4301-9DDE-9E73D2C3F7BA}");

            public partial struct Fields
            {
                public static readonly ID GroupMember = new ID("{631DA648-E2A5-4E3B-9733-C9C066C41EAE}");
                public const string GroupMember_FieldName = "Group Member";
                public static readonly ID Answer = new ID("{57F39C75-51F0-4888-903E-724DFDCC8A38}");
                public const string Answer_FieldName = "Answer";
                public static readonly ID Question = new ID("{9588B6D5-3E6A-4C16-BD37-98DA6F1DDE52}");
                public const string Question_FieldName = "Question";
            }
        }
    }
}