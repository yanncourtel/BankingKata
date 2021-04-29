Feature: An account

Scenario: Can print a statement after with a deposit transaction
	Given a client makes a deposit 500 euros on the 2021/04/27
	When she prints her bank statement
	Then she should see the following statement:
	"""
	Date		Amount		Balance
	27.4.2021		500		500
	"""

 Scenario: Can print a statement after with a withdrawal transaction
	Given a client withdraws 500 euros on the 2021/04/27
	When she prints her bank statement
	Then she should see the following statement:
	"""
	Date		Amount		Balance
	27.4.2021		-500		-500
	"""

 Scenario: Can print a statement with all transactions
	Given a client makes a deposit 500 euros on the 2021/04/20
	And a client withdraws 200 euros on the 2021/04/27
	When she prints her bank statement
	Then she should see the following statement:
	"""
	Date		Amount		Balance
	27.4.2021		-200		300
	20.4.2021		500		500
	"""

 Scenario: Can print a statement with no transactions
	When she prints her bank statement
	Then she should see the following statement:
	"""
	You have not made any transactions
	"""
