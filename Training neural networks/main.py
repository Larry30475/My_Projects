import pandas as pd
from bs4 import BeautifulSoup
from sklearn.metrics import mean_squared_error
from sklearn.model_selection import train_test_split
from sentence_transformers import SentenceTransformer
import numpy as np
import matplotlib.pyplot as plt
from sklearn.neural_network import MLPRegressor

model = SentenceTransformer('bert-base-cased')

jokes = []

for i in range(1, 101):
    file_name = f'jokes/init{i}.html'
    with open(file_name, 'r') as file:
        joke_html = file.read()
        soup = BeautifulSoup(joke_html, 'html.parser')
        joke_text = soup.find('font', size='+1').text.strip()
        jokes.append(joke_text)

embeddings = model.encode(jokes)

ratings = pd.DataFrame()

ratings_data = pd.read_excel(f'jester-data-1.xls', header=None, na_values=99)
ratings_data = ratings_data.iloc[:, 1:]
ratings = pd.concat([ratings_data, ratings], ignore_index=True)
ratings = ratings.mean()

X_train, X_val, y_train, y_val = train_test_split(embeddings, ratings, test_size=0.2, random_state=42)


def run(learning_rate_param=0.01, hidden_sizes=(100,), epochs=2000):
    mlp = MLPRegressor(solver='sgd', alpha=0.0, learning_rate='constant',
                       learning_rate_init=learning_rate_param, hidden_layer_sizes=hidden_sizes)
    train_loss = []
    val_loss = []

    for epoch in range(epochs):
        mlp.partial_fit(X_train, y_train)

        pred_y = mlp.predict(X_train)
        train_loss.append(mean_squared_error(y_train, pred_y) / 2)
        pred_y = mlp.predict(X_val)
        val_loss.append(mean_squared_error(y_val, pred_y) / 2)

    loss_curve = mlp.loss_curve_

    plt.plot(range(len(loss_curve)), loss_curve, label=f'Loss curve')
    plt.plot(range(len(train_loss)), train_loss, label=f'Train Loss')
    plt.plot(range(len(val_loss)), val_loss, label=f'Validation Loss')


learning_rate_list = [0.001, 0.01, 0.1]

for learning_rate in learning_rate_list:
    run(learning_rate_param=learning_rate)
    plt.legend()
    plt.title(f'MLP Learning Rate: {learning_rate}')
    plt.show()


hidden_sizes_list = [(100,), (200,), (500,)]

for hidden_sizes in hidden_sizes_list:
    run(hidden_sizes=hidden_sizes)
    plt.legend()
    plt.title(f'MLP Model Size: {hidden_sizes}')
    plt.show()


'''
run(epochs=1000)
plt.legend()
plt.show()

run(learning_rate_param=0.1)
plt.legend()
plt.show()

run(hidden_sizes=(500,))
plt.legend()
plt.show()
'''