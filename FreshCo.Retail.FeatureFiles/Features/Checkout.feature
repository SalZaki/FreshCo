#-----------------------------------
# Acceptance Criteria
#-----------------------------------
# Can I scan a valid product with sku and quantity?
# Can I scan multiple valid products with sku and quantities?
# Can I validate quantities?
# Can I validate totals?
# Can I apply discount(s)
# Can I validate discounted totals?
#-----------------------------------
# Functional Tests
#-----------------------------------

Feature: Checkout
	As a Supermarket checkout controller in FreshCo Retail fulfilment team,
	I want the ability to scan valid product(s), check their quantity and total,
	so that I can fulfill customer order(s)

@functional
Scenario: Configure check out service with porudtcs and discounts of scheme one and scanning a product
	Given I configure database context with following products
	| Id                                   | Name              | Sku | Price |
	| E8F89748-9E61-404A-BE81-558383019A9C | Gala Apple        | A   | 0.30  |
	| E2824E0D-DDDF-401A-AD63-C4063A69B342 | Organic Banana    | B   | 0.20  |
	| 0E97B027-297A-430C-9A8B-F3355DE57656 | Conference Pear   | C   | 0.40  |
	| 7F32CDD8-DB7E-454B-BC37-2EDA560B20D1 | Dates Deglet Nour | D   | 2.00  |
	| 6C90A5C1-EDB0-4066-8DDE-F2F4C496FBEB | Organic Eggs      | E   | 1.80  |
	And I configure database context with following discounts "scheme one"
	| Id                                   | Sku | Quantity | Value |
	| ABEB1FF1-7662-435F-B80A-55C5D300C488 | A   | 3        | 0.10  |
	| 75A3FE43-30BF-4A84-A5B7-5C44877C05DD | B   | 3        | 0.05  |
	| F73C013F-FC2E-4A91-B090-1675D09ED26E | C   | 3        | 0.10  |
	| B21B1146-5505-4022-B33C-5454ECDAAC32 | D   | 3        | 0.50  |
	| ECE44C23-6DE7-4ED2-90FA-AE31573048D3 | E   | 3        | 0.30  |
	And I configure checkout service
	When I scan a single product with a valid sku "A"
	Then I get the following product
	| Id                                   | Name       | Sku | Price |
	| E8F89748-9E61-404A-BE81-558383019A9C | Gala Apple | A   | 0.30  |

Scenario: Scanning multiple valid sku should return valid products and valid totals
	Given I configure database context with following products
	| Id                                   | Name              | Sku | Price |
	| E8F89748-9E61-404A-BE81-558383019A9C | Gala Apple        | A   | 0.30  |
	| E2824E0D-DDDF-401A-AD63-C4063A69B342 | Organic Banana    | B   | 0.20  |
	| 0E97B027-297A-430C-9A8B-F3355DE57656 | Conference Pear   | C   | 0.40  |
	| 7F32CDD8-DB7E-454B-BC37-2EDA560B20D1 | Dates Deglet Nour | D   | 2.00  |
	| 6C90A5C1-EDB0-4066-8DDE-F2F4C496FBEB | Organic Eggs      | E   | 1.80  |
	And I configure database context with following discounts "scheme one"
	| Id                                   | Sku | Quantity | Value |
	| ABEB1FF1-7662-435F-B80A-55C5D300C488 | A   | 3        | 0.10  |
	| 75A3FE43-30BF-4A84-A5B7-5C44877C05DD | B   | 3        | 0.05  |
	| F73C013F-FC2E-4A91-B090-1675D09ED26E | C   | 3        | 0.10  |
	| B21B1146-5505-4022-B33C-5454ECDAAC32 | D   | 3        | 0.50  |
	| ECE44C23-6DE7-4ED2-90FA-AE31573048D3 | E   | 3        | 0.30  |
	And I configure checkout service
	When I scan the following products with valid sku and quantities
	| Sku | Quantity |
	| A   | 5        |
	| B   | 3        |
	| C   | 5        |
	| D   | 1        |
	| E   | 1        |
	Then the following order lines are returned
	| Id                                   | Name              | Sku | Price | Quantity | Total |
	| E8F89748-9E61-404A-BE81-558383019A9C | Gala Apple        | A   | 0.30  | 5        | 1.50  |
	| E2824E0D-DDDF-401A-AD63-C4063A69B342 | Organic Banana    | B   | 0.20  | 3        | 0.60  |
	| 0E97B027-297A-430C-9A8B-F3355DE57656 | Conference Pears  | C   | 0.40  | 5        | 2.00  |
	| 7F32CDD8-DB7E-454B-BC37-2EDA560B20D1 | Dates Deglet Nour | D   | 2.00  | 1        | 2.00  |
	| 6C90A5C1-EDB0-4066-8DDE-F2F4C496FBEB | Organic Eggs      | E   | 1.80  | 1        | 1.80  |
	And the following order total is returned
	| Product Total | Price Total |
	| 15            | 7.90        |
	
Scenario: Applying dicsount to already scanned multiple valid sku should return valid products and valid totals
	Given I configure database context with following products
	| Id                                   | Name              | Sku | Price |
	| E8F89748-9E61-404A-BE81-558383019A9C | Gala Apple        | A   | 0.30  |
	| E2824E0D-DDDF-401A-AD63-C4063A69B342 | Organic Banana    | B   | 0.20  |
	| 0E97B027-297A-430C-9A8B-F3355DE57656 | Conference Pear   | C   | 0.40  |
	| 7F32CDD8-DB7E-454B-BC37-2EDA560B20D1 | Dates Deglet Nour | D   | 2.00  |
	| 6C90A5C1-EDB0-4066-8DDE-F2F4C496FBEB | Organic Eggs      | E   | 1.80  |
	And I configure database context with following discounts "scheme one"
	| Id                                   | Sku | Quantity | Value |
	| ABEB1FF1-7662-435F-B80A-55C5D300C488 | A   | 3        | 0.10  |
	| 75A3FE43-30BF-4A84-A5B7-5C44877C05DD | B   | 3        | 0.05  |
	| F73C013F-FC2E-4A91-B090-1675D09ED26E | C   | 3        | 0.10  |
	| B21B1146-5505-4022-B33C-5454ECDAAC32 | D   | 3        | 0.50  |
	| ECE44C23-6DE7-4ED2-90FA-AE31573048D3 | E   | 3        | 0.30  |
	And I configure checkout service
	And I scan the following products with valid sku and quantities
	| Sku | Quantity |
	| A   | 5        |
	| B   | 3        |
	| C   | 5        |
	| D   | 1        |
	| E   | 1        |
	When I apply already configured discounts "scheme one"
	Then the following discounted order total is returned
	| Product Total | Price Total |
	| 15            | 7.65        |
