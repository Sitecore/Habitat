using Sitecore.Data;

namespace Habitat.StandardContent
{
    public struct Templates
    {
        public struct HasSummary
        {
            public static ID ID => new ID("{A28A44A6-0DF1-49BF-83AD-CD21ABB9AF7E}");
            public struct Fields
            {
                public static ID Summary => new ID("{AC3FD4DB-8266-476D-9635-67814D91E901}");
            }
        }

        public struct HasBody
        {
            public static ID ID => new ID("{F15AFC99-281E-48DF-B333-91033622651A}");
            public struct Fields
            {
                public static ID Body => new ID("{D74F396D-5C5E-4916-BD0A-BFD58B6B1967}");
            }
        }
        public struct HasTitle
        {
            public static ID ID => new ID("{AF74A00B-8CA7-4C9A-A5C1-156A68590EE2}");
            public struct Fields
            {
                public static ID Title => new ID("{C30A013F-3CC8-4961-9837-1C483277084A}");
            }
        }
        public struct HasImage
        {
            public static ID ID => new ID("{640B55DD-405B-489C-A4AF-E328588EFE05}");
            public struct Fields
            {
                public static ID Image => new ID("{9492E0BB-9DF9-46E7-8188-EC795C4ADE44}");
            }
        }
    }
}
