namespace Sitecore.Foundation.Personalization.Pipelines.ProcessItem
{
    using Sitecore;
    using Sitecore.Analytics.Pipelines.ProcessItem;
    using Sitecore.Diagnostics;
    using Sitecore.Rules;

    public class RunRules : ProcessItemProcessor
    {
        public override void Process(ProcessItemArgs args)
        {
            Assert.ArgumentNotNull(args, "args");

            var item = Context.Item;

            var parentItem = item?.Database.GetItem(Personalization.Constants.ProcessItemRules);

            if (parentItem == null)
                return;

            var rules = RuleFactory.GetRules<RuleContext>(parentItem, "Rule");
            if (rules == null)
                return;

            var ruleContext = new RuleContext()
            {
                Item = item
            };
            rules.Run(ruleContext);
        }
    }
}