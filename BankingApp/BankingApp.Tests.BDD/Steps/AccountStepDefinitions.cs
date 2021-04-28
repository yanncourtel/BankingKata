using TechTalk.SpecFlow;

namespace BankingApp.Tests.BDD.Steps
{
    [Binding]
    public sealed class AccountStepDefinitions
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        public AccountStepDefinitions()
        {

        }

        [Given(@"a client makes a deposit (.*) euros on the (.*)/(.*)")]
        public void GivenAClientMakesADepositEurosOnThe(int p0, string p1, int p2)
        {
            
        }
        
        [When(@"she prints her bank statement")]
        public void WhenShePrintsHerBankStatement()
        {
            
        }
        
        [Then(@"she should see a statement with the following transactions:")]
        public void ThenSheShouldSeeAStatementWithTheFollowingTransactions(string multilineText)
        {
            
        }

    }
}
