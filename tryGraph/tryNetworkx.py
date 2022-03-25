import networkx as nx
import matplotlib.pyplot as plt

words = ['giraffe', 'elephant', 'ant', 'tiger', 'racoon', 'cat',
         'hedgehog', 'mouse']
if __name__ == '__main__':
    G = nx.DiGraph()
    G.add_nodes_from(words)

    for word1 in words:
        for word2 in words:
            if word1 != word2 and word1[-1] == word2[0]:
                G.add_edge(word1, word2)
    print(nx.algorithms.dag.dag_longest_path(G))
    nx.draw_networkx(G)
    plt.show()