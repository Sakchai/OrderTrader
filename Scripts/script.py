import settrade_v2
import urllib3
from settrade_v2 import Investor

class Order:
    def __init__(self, app_id, app_secret, app_code, broker_id, order_type, account_no, symbol, price, volume, side, pin, position=None, price_type=None, validity_type=None):
        print(f"app_id:{app_id}")
        print(f"app_secret:{app_secret}")
        print(f"app_code:{app_code}")
        print(f"broker_id:{broker_id}")
        self.app_id = app_id
        self.app_secret = app_secret
        self.app_code = app_code
        self.broker_id = broker_id
        self.order_type = order_type
        self.account_no = account_no
        self.symbol = symbol
        self.price = price
        self.volume = volume
        self.side = side
        self.pin = pin
        self.position = position
        self.price_type = price_type
        self.validity_type = validity_type
        
        self.investor = Investor(
            app_id= self.app_id,
            app_secret=self.app_secret,
            app_code=self.app_code,
            broker_id=self.broker_id,
            is_auto_queue=False
        )

    def place_order(self):
        print(f"app_id:{self.app_id}")
        if self.order_type == "Derivatives":
            deri = self.investor.Derivatives(account_no=self.account_no)
            order = deri.place_order(
                symbol=self.symbol,
                price=self.price,
                volume=self.volume,
                side=self.side,
                position=self.position,
                pin=self.pin,
                price_type=self.price_type,
                validity_type=self.validity_type
            )
        elif self.order_type == "Equity":
            equity = self.investor.Equity(account_no=self.account_no)
            order = equity.place_order(
                symbol=self.symbol,
                price=self.price,
                volume=self.volume,
                side=self.side,
                pin=self.pin
            )
        else:
            raise ValueError("Invalid order type. Must be 'Derivatives' or 'Equity'.")

        return order


def add(a, b):
    return a + b
