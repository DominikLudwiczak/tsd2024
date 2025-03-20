import urllib.request
import json
from datetime import datetime, timedelta
import pandas as pd

if __name__ == "__main__":
    BASE_URL = "http://api.nbp.pl/api/cenyzlota"
    start_date = datetime(2020, 1, 1)
    end_date = datetime.now()
    delta = timedelta(days=90)


    def fetch_data(start, end):
        url = f"{BASE_URL}/{start}/{end}/?format=json"
        try:
            with urllib.request.urlopen(url) as response:
                return json.loads(response.read().decode())
        except Exception as e:
            print(f"Error fetching data for {start} to {end}: {e}")
            return []


    all_data = []
    current_start = start_date

    while current_start < end_date:
        current_end = min(current_start + delta, end_date)
        data = fetch_data(current_start.strftime("%Y-%m-%d"), current_end.strftime("%Y-%m-%d"))
        all_data.extend(data)
        current_start = current_end + timedelta(days=1)

    df = pd.DataFrame(all_data)
    df['data'] = pd.to_datetime(df['data'])
    df.set_index('data', inplace=True)


    # SECOND TOP 10
    start_date = datetime(2019, 1, 1)
    end_date = datetime(2022, 12, 31)
    second10 = df[(df.index >= start_date) & (df.index <= end_date)].sort_values('cena', ascending=False).iloc[10:13]
    print(second10)

    # TOP 3
    top3 = df[df.index >= datetime.now() - timedelta(days=365)].sort_values('cena', ascending=False).iloc[:3]
    print(top3)
