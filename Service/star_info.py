import numpy as np
import pandas as pd

# def star_data(star_m, star_rad):

#     main_seq_data = pd.read_csv('data_range_stars.csv', sep=';')

#     star_lum_sun = 0.

#     if (star_m <= 0.43):
#         star_lum_sun = 0.23*star_m**2.3
#     elif (0.43 < star_m and star_m <= 2.):
#         star_lum_sun = star_m**4.
#     elif (2. < star_m and star_m <= 55):
#         star_lum_sun = 1.4*star_m**3.5
#     elif (55. < star_m):
#         star_lum_sun = 32000*star_m

#     sun_lum_IU = 3.828*(10**26)
#     steff_boltz = 5.67*(10**(-8))
#     star_rad_IU = star_rad*695510000

#     star_eff_T = (sun_lum_IU/(4.*np.pi*(star_rad_IU**2)*steff_boltz))**0.25

#     for i,row in main_seq_data.iterrows():
        
#         if i == 0:
#             if float(str(row[0]).replace(',', '.')) <= star_eff_T and \
#             float(str(row[2]).replace(',', '.')) <= star_m and star_m < float(str(row[3]).replace(',', '.')) and \
#             float(str(row[4]).replace(',', '.')) <= star_rad and star_rad < float(str(row[5]).replace(',', '.')) and \
#             float(str(row[6]).replace(',', '.')) <= star_lum_sun:
#                 return np.concatenate((np.array([star_eff_T, star_lum_sun, 1]), row[8:11].to_numpy()))

#         elif i == 6:
#             if star_eff_T < float(str(row[1]).replace(',', '.')) and \
#             float(str(row[2]).replace(',', '.')) <= star_m and star_m < float(str(row[3]).replace(',', '.')) and \
#             float(str(row[4]).replace(',', '.')) <= star_rad and star_rad < float(str(row[5]).replace(',', '.')) and \
#             star_lum_sun < float(str(row[6]).replace(',', '.')):
#                 return np.concatenate((np.array([star_eff_T, star_lum_sun, 1]), row[8:11].to_numpy()))

#         else:
#             if float(str(row[0]).replace(',', '.')) <= star_eff_T and star_eff_T < float(str(row[1]).replace(',', '.')) and \
#             float(str(row[2]).replace(',', '.')) <= star_m and star_m < float(str(row[3]).replace(',', '.')) and \
#             float(str(row[4]).replace(',', '.')) <= star_rad and star_rad < float(str(row[5]).replace(',', '.')) and \
#             float(str(row[6]).replace(',', '.')) <= star_lum_sun and star_lum_sun < float(str(row[7]).replace(',', '.')):
#                 return np.concatenate((np.array([star_eff_T, star_lum_sun, 1]), row[8:11].to_numpy()))

#     return np.array([0, 0, 0, 0, 0, 0])


def star_data(star_m, star_rad):

    main_seq_data = pd.read_csv('data_range_stars.csv', sep=';')

    for i,row in main_seq_data.iterrows():
        
        if i == 0:
            if float(str(row[2]).replace(',', '.')) <= star_m and star_m < float(str(row[3]).replace(',', '.')) and \
               float(str(row[4]).replace(',', '.')) <= star_rad and star_rad < float(str(row[5]).replace(',', '.')):
                star_eff_T = float(str(row[0]).replace(',', '.'))
                star_lum_sun = float(str(row[6]).replace(',', '.'))
                return np.concatenate((np.array([star_eff_T, star_lum_sun, 1]), row[8:11].to_numpy()))

        elif i == 6:
            if float(str(row[2]).replace(',', '.')) <= star_m and star_m < float(str(row[3]).replace(',', '.')) and \
               float(str(row[4]).replace(',', '.')) <= star_rad and star_rad < float(str(row[5]).replace(',', '.')):
                star_eff_T = float(str(row[1]).replace(',', '.'))
                star_lum_sun = float(str(row[5]).replace(',', '.'))
                return np.concatenate((np.array([star_eff_T, star_lum_sun, 1]), row[8:11].to_numpy()))

        else:
            if float(str(row[2]).replace(',', '.')) <= star_m and star_m < float(str(row[3]).replace(',', '.')) and \
               float(str(row[4]).replace(',', '.')) <= star_rad and star_rad < float(str(row[5]).replace(',', '.')):
                star_eff_T = (float(str(row[1]).replace(',', '.')) + float(str(row[0]).replace(',', '.')))/2
                star_lum_sun = (float(str(row[7]).replace(',', '.')) + float(str(row[6]).replace(',', '.')))/2
                return np.concatenate((np.array([star_eff_T, star_lum_sun, 1]), row[8:11].to_numpy()))

    return np.array([0, 0, 0, 0, 0, 0])


