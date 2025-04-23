import sys
import json
import pandas as pd
from sklearn.linear_model import LinearRegression
import numpy as np

# Read input data from stdin
input_data = json.load(sys.stdin)

df = pd.DataFrame(input_data)
df['Index'] = np.arange(len(df))  # Simulate time index

# Reshape for sklearn
X = df['Index'].values.reshape(-1, 1)
y = df['Temperature'].values

# Train simple linear regression
model = LinearRegression()
model.fit(X, y)

# Predict next value
next_index = np.array([[len(df)]])
prediction = model.predict(next_index)

# Output result
print(json.dumps({
    "PredictedTemperature": prediction[0]
}))
