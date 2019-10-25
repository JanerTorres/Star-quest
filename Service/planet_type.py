import numpy as np

mass_rock = [0.010, 0.032, 0.1, 0.32, 1.0, 3.16, 10.0, 31.6, 100, 316]
radius_rock = [[0.38, 0.33, 0.25, 0.24, 0.23, 0.19], [0.55, 0.48, 0.37, 0.34, 0.33, 0.27],
               [0.79, 0.69, 0.54, 0.50, 0.48, 0.39], [1.12, 0.97, 0.77, 0.71, 0.68, 0.55],
               [1.55, 1.36, 1.08, 1.00, 0.95, 0.77], [2.12, 1.85, 1.48, 1.36, 1.30, 1.04],
               [2.87, 2.48, 1.97, 1.80, 1.71, 1.36], [3.74, 3.23, 2.54, 2.31, 2.19, 1.72],
               [4.68, 4.03, 3.14, 2.84, 2.69, 2.09], [5.43, 4.67, 3.64, 3.29, 3.12, 2.42]]

planet_types = ["ice", "waterWorld", "rock", "earth", "ferrous", "pureIron"]


def find_nearest_index(array, value):
    array = np.asarray(array)
    idx = (np.abs(array - value)).argmin()
    return idx


def classify(planet_mass, planet_radius):
    if planet_radius > 6.5 or planet_mass > 320.:
        return "gas"
    else:
        closest_mass_index = find_nearest_index(mass_rock, planet_mass)
        closest_radius_index = find_nearest_index(radius_rock[closest_mass_index], planet_radius)

        planet_type = planet_types[closest_radius_index]

        return planet_types[closest_radius_index]
