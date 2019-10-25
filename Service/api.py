from flask import Flask, request
from flask_restful import Resource, Api
from hab_zone import HZ
from star_info import star_data
from planet_type import classify

app = Flask(__name__)
api = Api(app)

def switcher_planet_type(argument):
    switcher = {
        'ice': "00",
        'waterWorld': "10",
        'rock': "01",
        'earth': "11",
        'ferrous': "02",
        'pureIron': "03",
        'gas': "20"
    }
    return switcher.get(argument, "Invalid type")

# Ejemplo
class HelloWorld(Resource):
    def get(self):
        return {'about':'Hello World'}
    
    def post(self):
        some_json = request.get_json()
        return {'you sent': some_json},201

class StarData(Resource):
    def get(self, str_mass_rad):
        mass, rad = [float(num.replace(',', '.')) for num in str_mass_rad.split('&')]
        res = star_data(mass, rad).tolist()

        hab_zone = []
        ice_zone = []

        # if (res[2]):
        #     hab_zone = HZ(res[0], res[1])
        #     ice_zone = 2.7*(mass**2)
        hab_zone = [0,0,0,0]
        ice_zone = 0
        
        return {'T_eff': res[0],
                'lum_s': res[1],
                'type': res[5],
                'per_main_seq': res[3],
                'color': res[4],
                'valid': res[2],
                'hab_zone_min_radius': hab_zone[0],
                'hab_zone_max_radius': hab_zone[3],
                'ice_zone': ice_zone}

class PlanetType(Resource):
    def get(self, planet_data):
        planet_mass, planet_rad, star_temp, star_lum, star_mass, semi_major_axis = [float(num.replace(',', '.')) for num in planet_data.split('&')]
        planet_type = classify(planet_mass, planet_rad)

        # hab_zone = HZ(star_temp, star_lum)
        # hab_zone = [hab_zone[0], hab_zone[3]]

        ice_zone = 2.7*(star_mass**2)

        if semi_major_axis > ice_zone:
            planet_type = 'gas'
        
        # if planet_type == 'ice':
        #     if hab_zone[0] <= semi_major_axis and semi_major_axis <= hab_zone[1]:
        #         return {'type': switcher_planet_type('waterWorld')}
        #     elif semi_major_axis <= hab_zone[0]:
        #         return {'type': switcher_planet_type('rock')}

        # elif planet_type == 'waterWorld':
        #     if semi_major_axis < hab_zone[0]:
        #         return {'type': switcher_planet_type('rock')}
        #     elif hab_zone[1] < semi_major_axis:
        #         return {'type': switcher_planet_type('ice')}

        planet_type = switcher_planet_type(planet_type)
        return {'type': planet_type}

api.add_resource(HelloWorld, '/')
api.add_resource(StarData, '/stardata/<string:str_mass_rad>')
api.add_resource(PlanetType, '/planettype/<string:planet_data>')

if __name__=='__main__':
    app.run(host='0.0.0.0', port=8080, debug=True)