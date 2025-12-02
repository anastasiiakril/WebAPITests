Feature:CurrencyAPI



@tag1
Scenario: get convert usd to uah
	Given connected to currency data db
	And create convert request with amount "200" from "USD" to "UAH" for "2005-01-01"
	When send currency convert request
	Then conversion response is sucess
	And converted amount is returned

	 
@tag1
Scenario: get change of usd,eur relative to uah
	Given connected to currency data db
	And create change request with currencies "USD,EUR" relative to source "UAH" starting "2025-12-01" ending "2025-12-02"
	When send currency change request
	Then change response is sucess
	And change of the currencies is returned
