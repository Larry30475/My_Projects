from structure import *

if __name__ == '__main__':
    df = pd.read_csv('connection_graph.csv')
    df = df.drop(columns=["Unnamed: 0.1", "Unnamed: 0", "company"])

    gr = Graph()

    for ind, row in tqdm(df.iterrows(), total=273471):
        gr.load_data(*row.values.flatten().tolist())

    route, path = gr.TabuSearch('KRZYKI', ['Reja', 'Inżynierska', 'Katedra', 'pl. Orląt Lwowskich'], '13:00:00', True, iters=5)

    # route, path =  gr.TabuSearch('KRZYKI',['Reja','Inżynierska','Katedra','pl. Orląt Lwowskich'],'13:00:00',False)

    print(route)
    for line in path.split('\n'):
        print(line)

    # gr.eval_route('17:15:00', ['KRZYKI', 'Reja', 'RACŁAWICKA', 'KRZYKI'], True)
    # gr.eval_route('17:15:00', ['KRZYKI', 'Reja', 'RACŁAWICKA', 'KRZYKI'], False)
    # gr.astar('KRZYKI', 'Inżynierska', '17:15:00', time_criterion=True)
    # gr.astar('pl. Orląt Lwowskich', 'Inżynierska', '14:15:00', time_criterion=False)

    '''
    all_stops = df['end_stop'].unique()

    test = []

    for _ in range(10):
        pack = []
        for _ in range(random.randint(3, 6)):
            x = random.randint(0, len(all_stops))
            pack.append(all_stops[x])
        test.append(pack)
    test

    for test_case in test:
        route, path = gr.TabuSearch(test_case[0], test_case[1:], '10:00:00', True, iters=5)
        print(route)
        for line in path.split('\n'):
            print(line)
    '''
