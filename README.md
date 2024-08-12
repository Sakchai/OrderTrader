# C# .NET Core 8 Application with Python Integration for Settrade OpenAPI

This project integrate a Python script within a C# .NET Core 8 application to place orders using the Settrade OpenAPI. The Python script handles the interaction with the API, while the C# application manages the input and execution flow.

## Prerequisites

1. **.NET Core 8 SDK**: Ensure you have the .NET Core 8 SDK installed on your machine.
2. **Python 3.7 or higher**: Make sure you have Python installed. This project assumes you are using a virtual environment.
3. **Settrade OpenAPI**: Obtain the necessary credentials (app_id, app_secret, etc.) for accessing the Settrade OpenAPI.
4. **Python Packages**: Install the required Python packages using the `requirements.txt` provided.

## Setup

1. **Clone the Repository**:
   ```bash
   git clone  https://github.com/Sakchai/OrderTrader.git
   cd OrderTrader

2. **Set Up Python Virtual Environment**:
   ```bash
    python -m venv venv
    source venv/bin/activate  # On Windows: venv\Scripts\activate
    pip install -r requirements.txt

3. **Configure Environment Variables**:
    Update the PYTHONHOME and PYTHONPATH in PythonService.cs to point to your Python virtual environment path.

4. **Update Python Script Path**:
    Ensure the path to your Python script is correctly set in Program.cs:    
   ```bash
    string pythonScriptPath = @"c:\folder\script.py";

5. **Give a Star!** :star:
   If you like or are using this project to learn or start your solution, please give it a star. Thanks!
   Or if you're feeling really generous, we now support GitHub sponsorships - see the button above.

# Project Structure    

.
├── OrderTraderApp
│   ├── Services
│   │   ├── IOrderTradeService.cs
│   │   ├── OrderTradeService.cs
│   ├── Models
│   │   ├── Transaction.cs
│   ├── Program.cs
│   ├── OrderTrader.csproj
├── Scripts
│   ├── script.py
├── requirements.txt
├── README.md
