# UseCase2_NET_Senior_Dima_Panteliuk
# Validation Project

## Application Description

This application offers functionality related to interacting with the Stripe API for financial transactions. It includes several endpoints to retrieve balance information and transaction details. It has the following main endpoints:

1. **Get Balance** (`GET /api/stripe/balance`):
   This endpoint allows users to retrieve their current balance information. It communicates with the Stripe API's `BalanceService` to fetch the balance data. If successful, it returns the balance information.

2. **Get Transactions with Pagination** (`GET /api/stripe/transactions`):
   This endpoint allows users to retrieve a list of transactions with pagination support. Users can specify the number of items per page and the key of the last item from the previous page. It uses the `BalanceTransactionListOptions` to customize the list query and interacts with the `BalanceTransactionService` from the Stripe API. The endpoint returns a list of transaction data.

3. **Make Test Transaction** (`POST /api/stripe/testtransactions`):
   This endpoint is meant for testing purposes only. It allows users to simulate a test transaction by creating a charge using the Stripe API's `ChargeService`. It takes no input parameters and returns the result of the test charge creation.

## Running the Application Locally

To run the developed application locally, follow these steps:

1. **Prerequisites**:
   - Make sure you have the .NET Core SDK installed on your machine.
   - Set up your Stripe account and obtain the necessary API keys.

2. **Configure Stripe API Keys**:
   Open the `appsettings.json` file and replace the placeholder values with your Stripe API keys.

3. **Run the Application**:
   Using the terminal, navigate to the project directory and run the following command:
   ```
   dotnet run
   ```

5. **Access Endpoints**:
   Once the application is running, you can access the defined endpoints using URLs like:
   - `http://localhost:5000/swagger/index.html` (swagger)
   - `http://localhost:5000/api/stripe/balance`
   - `http://localhost:5000/api/stripe/transactions?pageSize=10&lastItemKey=lastKey`
   - `http://localhost:5000/api/stripe/testtransactions` (for testing)

## Example URLs for Endpoint Usage

1. **Get Balance**:
   - URL: `http://localhost:5000/api/stripe/balance`

2. **Get Transactions with Pagination**:
   - URL: `http://localhost:5000/api/stripe/transactions?pageSize=10&lastItemKey=lastKey`

3. **Make Test Transaction** (for testing, POST request):
   - URL: `http://localhost:5000/api/stripe/testtransactions`

Remember that the provided URLs assume that you're running the application locally on the default port (5000). Adjust the URL accordingly if you're using a different port.