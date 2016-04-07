Feature: Serialise users and roles


@NeedImplementation
Scenario: Serialise users and roles_UC1_Habitat modules roles available in Role Manager
	Given User is logged to sitecore as Admin
	When User selects Role Manager
	Then Following roles available
	| Role                                   |
	| habitat\Project Habitat Content Author |
	| habitat\Project Habitat Modules Admin  |
	| modules\Feature Accounts Admin         |
	| modules\Feature Demo Admin             |
	| modules\Feature FAQ Admin              |
	| modules\Feature Identity Admin         |
	| modules\Feature Language Admin         |
	| modules\Feature Maps Admin             |
	| modules\Feature Media Admin            |
	| modules\Feature Metadata Admin         |
	| modules\Feature Multisite Admin        |
	| modules\Feature Navigation Admin       |
	| modules\Feature News Admin             |
	| modules\Feature PageContent Admin      |
	| modules\Feature Person Admin           |
	| modules\Feature Search Admin           |
	| modules\Feature Social Admin           |
	| modules\Feature Teasers Admin          |
	| modules\Foundation Assets Admin        |
	| modules\Foundation Indexing Admin      |
	| modules\Foundation Multisite Admin     |


@NeedImplementation
Scenario: Serialise users and roles_UC2_Non-admin user without modules admin rights
	Given User with following role logged to Sitecore
	| Role                                   |
	| habitat\Project Habitat Content Author |
	And item based on following templates created under /sitecore/content/Habitat/
	| Base Templates           |
	| Standard template        |
	| _AccountSettings         |
	| _Interesr                |
	| _MailTemplate            |
	| _ProfileSettings         |
	| _CampaignToken           |
	| _DemoContent             |
	| _LinkToken               |
	| _ProfillingSettings      |
	| _TextToken               |
	| _Token                   |
	| _FAQ                     |
	| _FAQ Group               |
	| _Identity                |
	| _LanguageSettings        |
	| _MapPoint                |
	| Map Type                 |
	| Background Type          |
	| _HasMedia                |
	| _HasMediaImage           |
	| _HasMediaSelector        |
	| _HasMediaVideo           |
	| _HasParallaxBackground   |
	| _MediaSiteExtension      |
	| _Keyword                 |
	| _PageMetadata            |
	| _SiteMetadata            |
	| _SiteConfiguration       |
	| _Link                    |
	| _LinkMenuItem            |
	| _NavigationRoot          |
	| _Navigable               |
	| _NewsArticle             |
	| _HasPageContent          |
	| _Employee                |
	| _Person                  |
	| _Quote                   |
	| _SearchResults           |
	| _TwitterFeed             |
	| _OpenGraph               |
	| _Accordeon               |
	| _TeaserContent           |
	| Datasource Configuration |
	| _Site                    |
	| _SiteSettings            |
	| _IndexedItem             |
	| _RenderingAssets         |
	| _PageAssets              |
	| FAQ                      |
	| FAQ Group                |
	| Interest                 |                
	When User opens item in Content Editor
	And User clicks <Lock and Edit> link
	Then all item fields disabled


@NeedImplementation
Scenario: Serialise users and roles_UC3_Admin user with modules admin rights
	Given User with following role logged to Sitecore
	| Role                                   |
	| habitat\Project Habitat Content Author |
	| habitat\Project Habitat Modules Admin  |
	And item based on following templates created under /sitecore/content/Habitat/
	| Base Templates           |
	| Standard template        |
	| _AccountSettings         |
	| _Interesr                |
	| _MailTemplate            |
	| _ProfileSettings         |
	| _CampaignToken           |
	| _DemoContent             |
	| _LinkToken               |
	| _ProfillingSettings      |
	| _TextToken               |
	| _Token                   |
	| _FAQ                     |
	| _FAQ Group               |
	| _Identity                |
	| _LanguageSettings        |
	| _MapPoint                |
	| Map Type                 |
	| Background Type          |
	| _HasMedia                |
	| _HasMediaImage           |
	| _HasMediaSelector        |
	| _HasMediaVideo           |
	| _HasParallaxBackground   |
	| _MediaSiteExtension      |
	| _Keyword                 |
	| _PageMetadata            |
	| _SiteMetadata            |
	| _SiteConfiguration       |
	| _Link                    |
	| _LinkMenuItem            |
	| _NavigationRoot          |
	| _Navigable               |
	| _NewsArticle             |
	| _HasPageContent          |
	| _Employee                |
	| _Person                  |
	| _Quote                   |
	| _SearchResults           |
	| _TwitterFeed             |
	| _OpenGraph               |
	| _Accordeon               |
	| _TeaserContent           |
	| Datasource Configuration |
	| _Site                    |
	| _SiteSettings            |
	| _IndexedItem             |
	| _RenderingAssets         |
	| _PageAssets              |
	| FAQ                      |
	| FAQ Group                |
	| Interest                 |                
	When User opens item in Content Editor
	And User clicks <Lock and Edit> link
	Then all item fields enabled

	@NeedImplementation
Scenario: Serialise users and roles_UC4_Admin user with multisite admin rights
	Given User with following role logged to Sitecore
	| Role                                   |
	| habitat\Project Habitat Content Author |
	| modules\Feature Multisite Admin        |
	And item based on following templates created under /sitecore/content/Habitat/
	| Base Templates           |
	| Standard template        |
	| _AccountSettings         |
	| _Interesr                |
	| _MailTemplate            |
	| _ProfileSettings         |
	| _CampaignToken           |
	| _DemoContent             |
	| _LinkToken               |
	| _ProfillingSettings      |
	| _TextToken               |
	| _Token                   |
	| _FAQ                     |
	| _FAQ Group               |
	| _Identity                |
	| _LanguageSettings        |
	| _MapPoint                |
	| Map Type                 |
	| Background Type          |
	| _HasMedia                |
	| _HasMediaImage           |
	| _HasMediaSelector        |
	| _HasMediaVideo           |
	| _HasParallaxBackground   |
	| _MediaSiteExtension      |
	| _Keyword                 |
	| _PageMetadata            |
	| _SiteMetadata            |
	| _SiteConfiguration       |
	| _Link                    |
	| _LinkMenuItem            |
	| _NavigationRoot          |
	| _Navigable               |
	| _NewsArticle             |
	| _HasPageContent          |
	| _Employee                |
	| _Person                  |
	| _Quote                   |
	| _SearchResults           |
	| _TwitterFeed             |
	| _OpenGraph               |
	| _Accordeon               |
	| _TeaserContent           |
	| Datasource Configuration |
	| _Site                    |
	| _SiteSettings            |
	| _IndexedItem             |
	| _RenderingAssets         |
	| _PageAssets              |
	| FAQ                      |
	| FAQ Group                |
	| Interest                 |                
	When User opens item in Content Editor
	And User clicks <Lock and Edit> link
	Then following item fields enabled
	| Item name            |
	| Datasource Location  |
	| Datasource Template  |


