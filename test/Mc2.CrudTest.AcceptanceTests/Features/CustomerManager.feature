Feature: Customer Manager

As a an operator I wish to be able to Create, Update, Delete customers and list all customers
	
Scenario: Operator creates, list, update and delete customers 
	When User can create customer
	| FirstName | LastName | DateOfBirth | Email              | PhoneNumber   | BankAccountNumber  |
	| Ali       | Jahanbin | 1988-07-09  | jahanbin@yahoo.com | +989224957626 | NL91ABNA0417164300 |
	Then Operator can see 1 customer in get user list api result
	When Operator can update customer information
	| FirstName | LastName | DateOfBirth | Email              | PhoneNumber   | BankAccountNumber  |
	| AliAkbar       | Jahanbin | 1988-07-09  | jahanbin@yahoo.com | +989224957626 | NL91ABNA0417164300 |
	Then customer data updated successfully with following changes
	| FirstName | LastName | DateOfBirth | Email              | PhoneNumber   | BankAccountNumber  |
	| AliAkbar       | Jahanbin | 1988-07-09  | jahanbin@yahoo.com | +989224957626 | NL91ABNA0417164300 |
	When Operator delete customer information
	Then Customer data delete successfully
	When Operator get list of customer data the item count should be equal to 0