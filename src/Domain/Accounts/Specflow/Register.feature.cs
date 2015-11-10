﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.9.0.77
//      SpecFlow Generator Version:1.9.0.0
//      Runtime Version:4.0.30319.42000
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Habitat.Accounts.Specflow
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class RegisterFeature : Xunit.IUseFixture<RegisterFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "Register.feature"
#line hidden
        
        public RegisterFeature()
        {
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Register", "", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void TestInitialize()
        {
        }
        
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void SetFixture(RegisterFeature.FixtureData fixtureData)
        {
        }
        
        void System.IDisposable.Dispose()
        {
            this.ScenarioTearDown();
        }
        
        [Xunit.FactAttribute()]
        [Xunit.TraitAttribute("FeatureTitle", "Register")]
        [Xunit.TraitAttribute("Description", "Accounts_Register_UC1_Open Register page")]
        public virtual void Accounts_Register_UC1_OpenRegisterPage()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Accounts_Register_UC1_Open Register page", new string[] {
                        "NeedImplementation"});
#line 4
this.ScenarioSetup(scenarioInfo);
#line 5
 testRunner.Given("Habitat website is opened on Main Page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 6
 testRunner.When("Actor moves cursor over the User icon", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 7
 testRunner.And("User selects REGISTER from drop-down menu", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 8
 testRunner.Then("Page URL ends on /register", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 9
 testRunner.And("Register title presents on Register page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Field name"});
            table1.AddRow(new string[] {
                        "Email"});
            table1.AddRow(new string[] {
                        "Password"});
            table1.AddRow(new string[] {
                        "Confirm password"});
#line 10
 testRunner.And("Register fields present on Register page", ((string)(null)), table1, "And ");
#line 15
 testRunner.And("Register button presents", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute()]
        [Xunit.TraitAttribute("FeatureTitle", "Register")]
        [Xunit.TraitAttribute("Description", "Accounts_Register_UC2_Register a new user")]
        public virtual void Accounts_Register_UC2_RegisterANewUser()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Accounts_Register_UC2_Register a new user", new string[] {
                        "NeedImplementation"});
#line 19
this.ScenarioSetup(scenarioInfo);
#line 20
 testRunner.Given("Habitat website is opened on Register page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "Email",
                        "Password",
                        "Confirm password"});
            table2.AddRow(new string[] {
                        "kov@sitecore.net",
                        "k",
                        "k"});
#line 21
 testRunner.When("Actor enters following data in to the register fields", ((string)(null)), table2, "When ");
#line 24
 testRunner.And("Actor clicks Register button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 25
 testRunner.Then("Habitat website is opened on Main Page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "Button name"});
            table3.AddRow(new string[] {
                        "Logout"});
#line 26
 testRunner.And("Following buttons present under User drop-drop down menu", ((string)(null)), table3, "And ");
#line hidden
            TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                        "Button name"});
            table4.AddRow(new string[] {
                        "Login"});
            table4.AddRow(new string[] {
                        "Register"});
#line 29
 testRunner.And("Following buttons is no longer present under User drop-drop down menu", ((string)(null)), table4, "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute()]
        [Xunit.TraitAttribute("FeatureTitle", "Register")]
        [Xunit.TraitAttribute("Description", "Accounts_Register_UC3_Invalid email")]
        public virtual void Accounts_Register_UC3_InvalidEmail()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Accounts_Register_UC3_Invalid email", new string[] {
                        "InDesign"});
#line 36
this.ScenarioSetup(scenarioInfo);
#line 37
 testRunner.Given("Habitat website is opened on Register page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
            TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                        "Email",
                        "Password",
                        "Confirm password"});
            table5.AddRow(new string[] {
                        "kov$sitecore.net",
                        "k",
                        "k"});
#line 38
 testRunner.When("Actor enters following data in to the register fields", ((string)(null)), table5, "When ");
#line 41
 testRunner.And("Actor clicks Register button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                        "Email field error message"});
            table6.AddRow(new string[] {
                        ""});
#line 42
 testRunner.Then("System shows following message for the Email field", ((string)(null)), table6, "Then ");
#line 45
 testRunner.And("Habitat website is opened on Register page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute()]
        [Xunit.TraitAttribute("FeatureTitle", "Register")]
        [Xunit.TraitAttribute("Description", "Accounts_Register_UC4_Not unique email")]
        public virtual void Accounts_Register_UC4_NotUniqueEmail()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Accounts_Register_UC4_Not unique email", new string[] {
                        "InDesign"});
#line 49
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table7 = new TechTalk.SpecFlow.Table(new string[] {
                        "Email",
                        "Password",
                        "Confirm password"});
            table7.AddRow(new string[] {
                        "kov@sitecore.net",
                        "k",
                        "k"});
#line 50
 testRunner.Given("User with following data is registered", ((string)(null)), table7, "Given ");
#line 53
 testRunner.And("Habitat website is opened on Register page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table8 = new TechTalk.SpecFlow.Table(new string[] {
                        "Email",
                        "Password",
                        "Confirm password"});
            table8.AddRow(new string[] {
                        "kov@sitecore.net",
                        "k",
                        "k"});
#line 54
 testRunner.When("Actor enters following data in to the register fields", ((string)(null)), table8, "When ");
#line 57
 testRunner.And("Actor clicks Register button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table9 = new TechTalk.SpecFlow.Table(new string[] {
                        "Email field error message"});
            table9.AddRow(new string[] {
                        ""});
#line 58
 testRunner.Then("System shows following message for the Email field", ((string)(null)), table9, "Then ");
#line 61
 testRunner.And("Habitat website is opened on Register page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute()]
        [Xunit.TraitAttribute("FeatureTitle", "Register")]
        [Xunit.TraitAttribute("Description", "Accounts_Register_UC5_Incorrect confirm password")]
        public virtual void Accounts_Register_UC5_IncorrectConfirmPassword()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Accounts_Register_UC5_Incorrect confirm password", new string[] {
                        "InDesign"});
#line 65
this.ScenarioSetup(scenarioInfo);
#line 66
 testRunner.Given("Habitat website is opened on Register page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
            TechTalk.SpecFlow.Table table10 = new TechTalk.SpecFlow.Table(new string[] {
                        "Email",
                        "Password",
                        "Confirm password"});
            table10.AddRow(new string[] {
                        "kov@sitecore.net",
                        "k",
                        "a"});
#line 67
 testRunner.When("Actor enters following data in to the register fields", ((string)(null)), table10, "When ");
#line 70
 testRunner.And("Actor clicks Register button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table11 = new TechTalk.SpecFlow.Table(new string[] {
                        "Password field error message"});
            table11.AddRow(new string[] {
                        ""});
#line 71
 testRunner.Then("System shows following message for the Email field", ((string)(null)), table11, "Then ");
#line 74
 testRunner.And("Habitat website is opened on Register page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute()]
        [Xunit.TraitAttribute("FeatureTitle", "Register")]
        [Xunit.TraitAttribute("Description", "Accounts_Register_UC6_One of the required fields is empty")]
        public virtual void Accounts_Register_UC6_OneOfTheRequiredFieldsIsEmpty()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Accounts_Register_UC6_One of the required fields is empty", new string[] {
                        "InDesign"});
#line 78
this.ScenarioSetup(scenarioInfo);
#line 79
 testRunner.Given("Habitat website is opened on Register page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
            TechTalk.SpecFlow.Table table12 = new TechTalk.SpecFlow.Table(new string[] {
                        "Email",
                        "Password",
                        "Confirm password"});
            table12.AddRow(new string[] {
                        "",
                        "k",
                        "k"});
#line 80
 testRunner.When("Actor enters following data in to the register fields", ((string)(null)), table12, "When ");
#line 83
 testRunner.And("Actor clicks Register button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table13 = new TechTalk.SpecFlow.Table(new string[] {
                        "Required field error message"});
            table13.AddRow(new string[] {
                        ""});
#line 84
 testRunner.Then("System shows following message for the Email field", ((string)(null)), table13, "Then ");
#line 87
 testRunner.And("Habitat website is opened on Register page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute()]
        [Xunit.TraitAttribute("FeatureTitle", "Register")]
        [Xunit.TraitAttribute("Description", "Accounts_Register_UC7_all off the required fields are empty")]
        public virtual void Accounts_Register_UC7_AllOffTheRequiredFieldsAreEmpty()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Accounts_Register_UC7_all off the required fields are empty", new string[] {
                        "InDesign"});
#line 90
this.ScenarioSetup(scenarioInfo);
#line 91
 testRunner.Given("Habitat website is opened on Register page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
            TechTalk.SpecFlow.Table table14 = new TechTalk.SpecFlow.Table(new string[] {
                        "Email",
                        "Password",
                        "Confirm password"});
            table14.AddRow(new string[] {
                        "",
                        "",
                        ""});
#line 92
 testRunner.When("Actor enters following data in to the register fields", ((string)(null)), table14, "When ");
#line 95
 testRunner.And("Actor clicks Register button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table15 = new TechTalk.SpecFlow.Table(new string[] {
                        "Required field error message"});
            table15.AddRow(new string[] {
                        ""});
#line 96
 testRunner.Then("System shows following message for the Email field", ((string)(null)), table15, "Then ");
#line 99
 testRunner.And("Habitat website is opened on Register page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                RegisterFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                RegisterFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
