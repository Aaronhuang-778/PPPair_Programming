#!/usr/bin/env python 
# -*- coding:utf-8 -*-
import os
from typing import List

from random_word import RandomWords
import networkx as nx
import re
import random
import numpy as np

maxNum = 20

def save_data(folder, word_list, path):
    i = 0
    while os.path.exists(folder + '\\test' + str(i) + '.txt'):
        i += 1
    f = open(folder + '\\test' + str(i) + '.txt', 'w')
    f.write('\n'.join(word_list))
    f = open(folder + '\\result' + str(i) + '.txt', 'w')
    f.write('\n'.join(path))

if __name__ == '__main__':

    for num in range(maxNum):
        print("【第" + str(num) + "次】")

        limit = int(random.random() * 15 + 5)
        print(limit)

        r = RandomWords()
        word_list = r.get_random_words(hasDictionaryDef="true",
                                       minDictionaryCount=1, maxDictionaryCount=10,
                                       minLength=2, maxLength=10,
                                       limit=limit)
        print(word_list)
        regex = re.compile('[^a-zA-Z]+')
        for i in range(len(word_list)):
            word_list[i] = regex.sub('', word_list[i])

        # f = open('test' + str(num + savedNum) + '.txt', 'w')
        # f.write('\n'.join(word_list))

        G = nx.DiGraph()
        G.add_nodes_from(word_list)
        for word1 in word_list:
            for word2 in word_list:
                if word1 != word2 and word1[-1] == word2[0]:
                    G.add_edge(word1, word2, weight=len(word2))
        try:
            res = nx.find_cycle(G)
            print("circle!!!!!!!")
            print(res)
            # for u, v, d in G.edges(data=True):
            #     d['weight'] = -1
            #     G.add_edge(v, u, weight=100000)
            # path = nx.algorithms.approximation.traveling_salesman.\
            #     traveling_salesman_problem(G, method='christofides')
            longest_path = []
            length = 0
            for word1 in word_list:
                for word2 in word_list:
                    pathes = list(nx.all_simple_paths(G, word1, word2))
                    if (len(pathes) == 0):
                        continue
                    print(pathes)
                    for path in pathes:
                        if (len(path) > length):
                            longest_path = path
                            length = len(path)
            save_data('c_circle', word_list, longest_path)

        except nx.exception.NetworkXUnfeasible:
            print("no circle")
            # all_path = []
            # for word1 in word_list:
            #     for word2 in word_list:
            #         pathes = list(nx.all_simple_paths(G, word1, word2))
            #         if (len(pathes) == 0):
            #             continue
            #         print(pathes)
            #         for path in pathes:
            #             pathStr = ' '.join(path)
            #             all_path.append(pathStr)
            # save_data('n_no_circle', word_list, all_path)
            path = nx.algorithms.dag.dag_longest_path(G)
            print(path)
            save_data('c_no_circle', word_list, path)
