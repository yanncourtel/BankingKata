Feature: An account

Scenario: Can print a statement after with a deposit transaction
	Given a client makes a deposit 500 euros on the 2021/04/27
	When she prints her bank statement
	Then she should see a statement with the following transactions:
	"""
	| Date       | Amount | RunningBalance |
	| 2021/04/27 | +500   | 500            |
	"""

 