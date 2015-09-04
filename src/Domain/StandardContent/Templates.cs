using Sitecore.Data;

namespace Habitat.StandardContent
{
    public struct Templates
    {
        public static readonly ID Teaser = new ID("{C7D9D293-4EF8-4380-8E10-C4632E729F39}");

        public struct HasSummary
        {
            public static ID ID = new ID("{A28A44A6-0DF1-49BF-83AD-CD21ABB9AF7E}");

            public struct Fields
            {
                public static readonly ID Summary = new ID("{AC3FD4DB-8266-476D-9635-67814D91E901}");
            }
        }

        public struct HasAccordeon
        {
            public static ID ID = new ID("{C7D9D293-4EF8-4380-8E10-C4632E729F39}");

            public struct Fields
            {
                public static readonly ID AccordeonSelector = new ID("{9E942565-677F-491C-A0AC-6B930E37342A}");
            }
        }

        public struct HasBody
        {
            public static ID ID = new ID("{F15AFC99-281E-48DF-B333-91033622651A}");

            public struct Fields
            {
                public static readonly ID Body = new ID("{D74F396D-5C5E-4916-BD0A-BFD58B6B1967}");
            }
        }

        public struct HasLink
        {
            public static ID ID = new ID("{FEC0E62A-01FD-40E5-88F3-E5229FE79527}");

            public struct Fields
            {
                public static readonly ID Link = new ID("{E8AB122C-6F54-4D4E-AEC6-F81ADDC558FC}");
            }
        }

        public struct HasTitle
        {
            public static ID ID = new ID("{AF74A00B-8CA7-4C9A-A5C1-156A68590EE2}");

            public struct Fields
            {
                public static readonly ID Title = new ID("{C30A013F-3CC8-4961-9837-1C483277084A}");
            }
        }

        public struct HasImage
        {
            public static ID ID = new ID("{640B55DD-405B-489C-A4AF-E328588EFE05}");

            public struct Fields
            {
                public static readonly ID Image = new ID("{9492E0BB-9DF9-46E7-8188-EC795C4ADE44}");
            }
        }
    }
}