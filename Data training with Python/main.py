import pandas as pd
import numpy as np
from sklearn.model_selection import train_test_split
from sklearn.preprocessing import StandardScaler, MinMaxScaler, Normalizer
from sklearn.decomposition import PCA
from sklearn.naive_bayes import GaussianNB
from sklearn.tree import DecisionTreeClassifier
from sklearn.metrics import accuracy_score, precision_score, recall_score, confusion_matrix

# Task 1: Data Exploration
data = pd.read_csv('glass.data')  # Replace with the path to your 'glass.data' file
data.columns = ['Id', 'RI', 'Na', 'Mg', 'Al', 'Si', 'K', 'Ca', 'Ba', 'Fe', 'Class']
print(data.describe())
print(data.head())

print()

# Task 2: Data Preparation
X = data.drop(['Id', 'Class'], axis=1)
y = data['Class']
X_train, X_val, y_train, y_val = train_test_split(X, y, test_size=0.2, random_state=42)

# Without data processing
clf = DecisionTreeClassifier(random_state=42)
clf.fit(X_train, y_train)
y_pred = clf.predict(X_val)
print("Results without data processing:")
print("Accuracy:", accuracy_score(y_val, y_pred))
print("Precision:", precision_score(y_val, y_pred, average='weighted'))
print("Recall:", recall_score(y_val, y_pred, average='weighted'))
print("Confusion Matrix:\n", confusion_matrix(y_val, y_pred))

print()

# With data processing - Standardization
scaler = StandardScaler()
X_train_std = scaler.fit_transform(X_train)
X_val_std = scaler.transform(X_val)

clf_std = DecisionTreeClassifier(random_state=42)
clf_std.fit(X_train_std, y_train)
y_pred_std = clf_std.predict(X_val_std)
print("Results with data processing - Standardization:")
print("Accuracy:", accuracy_score(y_val, y_pred_std))
print("Precision:", precision_score(y_val, y_pred_std, average='weighted'))
print("Recall:", recall_score(y_val, y_pred_std, average='weighted'))
print("Confusion Matrix:\n", confusion_matrix(y_val, y_pred))

print()

# With data processing - Normalizer
normalizer = Normalizer()
X_train_norm = normalizer.fit_transform(X_train)
X_val_norm = normalizer.transform(X_val)

clf_norm = DecisionTreeClassifier(random_state=42)
clf_norm.fit(X_train_norm, y_train)
y_pred_norm = clf_norm.predict(X_val_norm)
print("Results with data processing - Normalization:")
print("Accuracy:", accuracy_score(y_val, y_pred_norm))
print("Precision:", precision_score(y_val, y_pred_norm, average='weighted'))
print("Recall:", recall_score(y_val, y_pred_norm, average='weighted'))
print("Confusion Matrix:\n", confusion_matrix(y_val, y_pred_norm))

print()

# Task 3: Classification - Naive Bayes
nb_clf = GaussianNB()
nb_clf.fit(X_train, y_train)
nb_y_pred = nb_clf.predict(X_val)
print("Naive Bayes Classifier:")
print("Accuracy:", accuracy_score(y_val, nb_y_pred))
print("Precision:", precision_score(y_val, nb_y_pred, average='weighted'))
print("Recall:", recall_score(y_val, nb_y_pred, average='weighted'))
print("Confusion Matrix:\n", confusion_matrix(y_val, nb_y_pred))

print()

# Task 3: Classification - Decision Tree with different hyperparameters
clf_params = [{'max_depth': None},
              {'max_depth': 5},
              {'max_depth': 10}]
for params in clf_params:
    clf = DecisionTreeClassifier(random_state=42, **params)
    clf.fit(X_train, y_train)
    y_pred = clf.predict(X_val)
    print("Decision Tree Classifier with Hyperparameters:", params)
    print("Accuracy:", accuracy_score(y_val, y_pred))
    print("Precision:", precision_score(y_val, y_pred, average='weighted'))
    print("Recall:", recall_score(y_val, y_pred, average='weighted'))
    print("Confusion Matrix:\n", confusion_matrix(y_val, y_pred))
    print()