import pandas as pd

import math

from datetime import datetime, timedelta

from typing import Tuple

from tqdm import tqdm

import random

class Node:

    def __init__(self, name, x, y) -> None:
        self.name = name
        self.x = x
        self.y = y
        self.connections = set()

    def __repr__(self) -> str:
        return (f"Station({self.name})")

    def get_closest_connections(self, at_time, lines_important=True):
        out_list = dict()

        if lines_important:
            for possible_conn in self.connections:
                if possible_conn[2] >= at_time:
                    if (possible_conn[0], possible_conn[1]) in out_list:

                        if out_list[(possible_conn[0], possible_conn[1])][2] > possible_conn[2]:
                            out_list[(possible_conn[0], possible_conn[1])] = possible_conn
                    else:
                        out_list[(possible_conn[0], possible_conn[1])] = possible_conn
        else:
            for possible_conn in self.connections:
                if possible_conn[2] >= at_time:
                    if (possible_conn[0]) in out_list:

                        if out_list[possible_conn[0]][2] > possible_conn[2]:
                            out_list[possible_conn[0]] = possible_conn
                    else:
                        out_list[possible_conn[0]] = possible_conn

        return set(out_list.values())


class Graph:

    def __init__(self) -> None:

        self.nodes = dict()

    def prep_path(self, path) -> str:
        return "\n".join([' To {} ---> {} : {} - {}'.format(x[0], x[1], x[2].strftime('%H:%M:%S'),
                                                                            x[3].strftime('%H:%M:%S')) for x in path])

    def nodes_distance(self, node1: Node, node2: Node):
        return math.sqrt(math.pow(node1.x - node2.x, 2) + math.pow(node1.y - node2.y, 2))

    def load_data(self, line, departure_time, arrival_time, start_stop, end_stop, start_stop_lat, start_stop_lon,
                  end_stop_lat, end_stop_lon) -> None:

        if start_stop not in self.nodes:
            self.nodes[start_stop] = Node(start_stop, start_stop_lat, start_stop_lon)

        if end_stop not in self.nodes:
            self.nodes[end_stop] = Node(end_stop, end_stop_lat, end_stop_lon)

        self.nodes[start_stop].connections.add((self.nodes[end_stop], line,
                                                datetime.strptime(departure_time, "%H:%M:%S"),
                                                datetime.strptime(arrival_time, "%H:%M:%S")))

    def eval_route(self, start_time, route, time_criterion=False):

        if time_criterion:

            total_time = timedelta(seconds=0)
            total_inst = ''
            curr_time = start_time
            for ind in range(1, len(route)):
                res = self.astar(start=route[ind - 1], end=route[ind], start_time=curr_time, time_criterion=True)
                if res is None:
                    return timedelta(days=9999), ""
                time_cost, arrival_time, instr = res
                total_time += time_cost
                total_inst += instr
                curr_time = arrival_time
            return total_time, total_inst

        else:

            total_transfers = 0
            total_inst = ''

            curr_time = start_time
            for ind in range(1, len(route)):
                res = self.astar(start=route[ind - 1], end=route[ind], start_time=curr_time, time_criterion=False)
                if res is None:
                    return 9999999, ""
                transfers, arrival_time, instr = res
                total_transfers += transfers
                total_inst += instr
                curr_time = arrival_time
            return total_transfers, total_inst

    def TabuSearch(self, start, list_of_stops, start_time, time_criterion=False, iters=10,
                   random_neighborhood_gens=False):

        tabu_size = len(list_of_stops) + 2 // 4

        # Origin : https://www.researchgate.net/publication/220471567_Optimizing_tabu_list_size_for_the_traveling_salesman_problem
        # Taken from:  
        # https://www.researchgate.net/publication/368159076_Multi-Tour_Set_Traveling_Salesman_Problem_in_Planning_Power_Transmission_Line_Inspection
        # they used this ratio, seems good 

        def swap_stations(route, station1, station2):
            out_route = route.copy()
            out_route[station1], out_route[station2] = out_route[station2], out_route[station1]
            return out_route

        # Source https://www.researchgate.net/publication/255599657_Analysis_of_neighborhood_generation_and_move_selection_strategies_on_the_performance_of_Tabu_Search
        def gen_neighborhood_routes_pairwise_interchange(route):

            out_routes = []

            for i in range(1, len(route) - 1):
                for j in range(1, len(route) - 1):
                    if i == j:
                        continue

                    out_routes.append(swap_stations(route, i, j))

            return out_routes

        # Source https://www.researchgate.net/publication/255599657_Analysis_of_neighborhood_generation_and_move_selection_strategies_on_the_performance_of_Tabu_Search
        def gen_neighborhood_routes_random_neighborhood(route):

            out_routes = []

            numbers_size = random.randint(1, len(route) - 1)

            for _ in range(numbers_size):

                curr_pos = random.randint(1, len(route) - 1)

                next_pos = random.randint(1, len(route) - 1)

                if curr_pos != next_pos:
                    out_routes.append(swap_stations(route, curr_pos, next_pos))

            return out_routes

        best_route = [start, *list_of_stops, start]
        best_route_eval, best_route_inst = self.eval_route(start_time, best_route, time_criterion)
        best_in_hood = best_route

        tabu = []

        for _ in tqdm(range(iters)):

            if random_neighborhood_gens:
                hood = gen_neighborhood_routes_random_neighborhood(best_in_hood)
            else:

                hood = gen_neighborhood_routes_pairwise_interchange(best_in_hood)

            best_in_hood = hood[0]
            best_in_hood_eval, best_in_hood_inst = self.eval_route(start_time, best_in_hood, time_criterion)

            for cand_route in hood:

                cand_eval, cand_inst = self.eval_route(start_time, cand_route, time_criterion)
                if (cand_eval < best_in_hood_eval) and cand_route not in tabu:
                    best_in_hood = cand_route
                    best_in_hood_eval = cand_eval
                    best_in_hood_inst = cand_inst

                # Aspiration criterion  
                if cand_route in tabu and cand_eval < best_route_eval:
                    best_in_hood = cand_route
                    best_in_hood_eval = cand_eval
                    best_in_hood_inst = cand_inst

            if best_in_hood_eval < best_route_eval:
                best_route = best_in_hood
                best_route_eval = best_in_hood_eval
                best_route_inst = best_in_hood_inst

            tabu.append(best_in_hood)

            if len(tabu) > tabu_size:
                tabu.pop(0)

        return best_route, best_route_inst

    def astar(self, start, end, start_time, time_criterion=False, ):

        curr_time = datetime.strptime(start_time, "%H:%M:%S")

        if time_criterion:

            target_not_found = True

            nodes_distances = dict()
            nodes_time = dict()
            nodes_connections_path = dict()

            visited = set()

            visited.add(start)
            nodes_time[start] = 0
            nodes_connections_path[start] = []

            for key, node in self.nodes.items():
                nodes_distances[key] = self.nodes_distance(node, self.nodes[end])

            for first_nodes in self.nodes[start].get_closest_connections(curr_time, False):

                if start == first_nodes[0].name:
                    continue

                time_to_visit = (first_nodes[3] - curr_time).seconds

                nodes_time[first_nodes[0].name] = time_to_visit

                nodes_connections_path[first_nodes[0].name] = nodes_connections_path[start].copy()
                nodes_connections_path[first_nodes[0].name].append(first_nodes)

            while target_not_found:

                next_node = None
                next_node_score = None

                for key in nodes_time.keys():
                    if key not in visited:
                        next_node_candidate_score = nodes_distances[key] * 1000 + nodes_time[key]
                        if next_node is None or next_node_score > next_node_candidate_score:
                            next_node = key
                            next_node_score = next_node_candidate_score

                visited.add(next_node)

                if next_node is None:
                    return None

                for explored_nodes_con in self.nodes[next_node].get_closest_connections(
                        curr_time + timedelta(seconds=nodes_time[next_node]), False):

                    if next_node == explored_nodes_con[0].name:
                        continue

                    nodes_time[explored_nodes_con[0].name] = (explored_nodes_con[3] - curr_time).seconds

                    nodes_connections_path[explored_nodes_con[0].name] = nodes_connections_path[next_node].copy()
                    nodes_connections_path[explored_nodes_con[0].name].append(explored_nodes_con)

                    if explored_nodes_con[0].name == end:
                        out = "\n"
                        out += f'\nFrom {start} '
                        out += self.prep_path(nodes_connections_path[end])
                        out += "\nTime taken : {}".format(timedelta(seconds=nodes_time[explored_nodes_con[0].name]))
                        out += "\nArrival at : {} ".format(
                            (curr_time + timedelta(seconds=nodes_time[explored_nodes_con[0].name])).strftime(
                                "%H:%M:%S"))
                        return (timedelta(seconds=nodes_time[explored_nodes_con[0].name]),
                                (curr_time + timedelta(seconds=nodes_time[explored_nodes_con[0].name])).strftime(
                                    "%H:%M:%S"), out)

        else:
            # node line dep arr

            def explore_line(node, depth, target_node, time, line, visited_list):

                if depth >= 2:
                    for conns in node.get_closest_connections(time, lines_important=True):
                        res = explore_line(conns[0], depth - 1, target_node, conns[3], conns[1], [node])
                        if res is not None:
                            resc = res.copy()
                            resc.append(conns)
                            return resc

                if depth == 1:
                    for conns in node.get_closest_connections(time, lines_important=True):
                        if conns[0] in visited_list:
                            continue
                        if conns[1] == line:
                            if conns[0].name == target_node:
                                return [conns]
                            vl = visited_list.copy()
                            vl.append(node)
                            res = explore_line(conns[0], 1, target_node, conns[3], line, vl)
                            if res is None:
                                return res
                            else:
                                resc = res.copy()
                                resc.append(conns)
                                return resc
                    return None

            res = None
            for lines_limit in range(1, 4):
                for cons in self.nodes[start].get_closest_connections(curr_time, lines_important=True):
                    res = explore_line(self.nodes[start], lines_limit, end, curr_time, cons[1], [])
                    if res is not None:
                        break
                if res is not None:
                    break
            if res is not None:
                res.reverse()
                out = "\n"
                out += f'\nFrom {start} '
                out += self.prep_path(res)
                out += "\nTime taken : {}".format(res[-1][3] - curr_time)
                out += "\nArrival at : {} ".format(res[-1][3].strftime("%H:%M:%S"))
                return (lines_limit, res[-1][3].strftime("%H:%M:%S"), out)
            else:
                return None