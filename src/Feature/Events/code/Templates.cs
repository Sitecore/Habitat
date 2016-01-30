namespace Sitecore.Feature.Events
{
    using Sitecore.Data;

    public struct Templates
    {
        public struct Event
        {
            public static ID ID = new ID("{1CD22D18-F739-4E95-A359-2F52288871DA}");

            public struct EventSettings
            {
                public static readonly ID GoogleMapsApiKey = new ID("{3C27D3B4-82BC-409F-9587-59BA4A9E0EF9}");
            }

            public struct Fields
            {
                public static readonly ID Title = new ID("{E7088D4F-9396-412D-9BD8-C9BA4096C79D}");
                public const string Title_FieldName = "EventTitle";

                public static readonly ID Image = new ID("{F48585E9-4984-493E-9F3B-F40710CAC667}");
                public const string Image_FieldName = "EventImage";

                public static readonly ID Description = new ID("{8D4A615D-8CB1-4324-A5FE-FCA3BE62044D}");
                public const string Description_FieldName = "EventDescription";

                public static readonly ID StartDate = new ID("{E2D7BDFF-048E-42BA-9F33-41D761E51B57}");
                public const string StartDate_FieldName = "EventEndDate";

                public static readonly ID EndDate = new ID("{FBFC769D-534C-4680-845A-CD5F226DD59C}");
                public const string EndDate_FieldName = "EventEndDate";

                public static readonly ID Location = new ID("{384E0CE5-7B42-4D00-A063-B903A07DD1B7}");
                public const string Location_FieldName = "Location";
            }
        }

        public struct EventFolder
        {
            public static readonly ID ID = new ID("{299C46F5-4AFC-4FE3-894F-D3E17CD159A8}");
        }
    }

    public struct Settings
    {
        public struct PageEvents
        {
            public static ID EventSavedToCalendar = new ID("{7106E950-89E5-40D3-A539-B8F7B9A9CB18}");
            public const string EventSavedToCalendar_ItemName = "Event saved to calendar";

        }
    }
}