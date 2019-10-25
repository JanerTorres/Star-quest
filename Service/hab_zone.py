# ************************************************************************************
# ************************************************************************************
#
#   espacio para documentación
#
# ************************************************************************************
# ************************************************************************************

import math

sRecentV = 1.776
caRecentV = 2.136e-4
cbRecentV = 2.533e-8
ccRecentV = -1.332e-11
cdRecentV = -3.097e-15

sRunawayG = 1.107
caRunawayG = 1.332e-4
cbRunawayG = 1.580e-8
ccRunawayG = -8.308e-12
cdRunawayG = -1.931e-15

SEsRunawayG = 1.188
SEcaRunawayG = 1.433e-4
SEcbRunawayG = 1.707e-8
SEccRunawayG = -8.968e-12
SEcdRunawayG = -2.084e-15

SubsRunawayG = 0.990
SubcaRunawayG = 1.209e-4
SubcbRunawayG = 1.404e-8
SubccRunawayG = -7.418e-12
SubcdRunawayG = -1.713e-15

sMaximumG = 0.356
caMaximumG = 6.171e-5
cbMaximumG = 1.698e-9
ccMaximumG = -3.198e-12
cdMaximumG = -5.575e-16

sEarlyMars = 0.3207
caEarlyMars = 5.5471e-5
cbEarlyMars = 1.5265e-9
ccEarlyMars = -2.874e-12
cdEarlyMars = -5.011e-16

# 5780, 1

def HZ(teff, lum):
    tags = ["recentV", "RunawayG", "MaximumG", "EarlyMars", "SERunawayG", "SubRunawayG", "recentVdis",
            "RunawayGdis", "MaximumGdis", "EarlyMarsdis", "SERunawayGdis", "SubRunawayGdis"]
    data = []
    recentV = sRecentV + caRecentV * (teff - 5780) + cbRecentV * math.pow(teff - 5780, 2) + ccRecentV * math.pow(
        teff - 5780, 3) + cdRecentV * math.pow(teff - 5780, 4)
    recentV = ((round((recentV - math.floor(recentV)) * 1000)) / 1000) + math.floor(recentV)
    data.append(recentV)
    RunawayG = sRunawayG + caRunawayG * (teff - 5780) + cbRunawayG * math.pow(teff - 5780, 2) + ccRunawayG * math.pow(
        teff - 5780, 3) + cdRunawayG * math.pow(teff - 5780, 4)
    RunawayG = ((round((RunawayG - math.floor(RunawayG)) * 10000)) / 10000) + math.floor(RunawayG)
    data.append(RunawayG)
    MaximumG = sMaximumG + caMaximumG * (teff - 5780) + cbMaximumG * math.pow(teff - 5780, 2) + ccMaximumG * math.pow(
        teff - 5780, 3) + cdMaximumG * math.pow(teff - 5780, 4)
    MaximumG = ((round((MaximumG - math.floor(MaximumG)) * 1000)) / 1000) + math.floor(MaximumG)
    data.append(MaximumG)
    EarlyMars = sEarlyMars + caEarlyMars * (teff - 5780) + cbEarlyMars * math.pow(teff - 5780,
                                                                                  2) + ccEarlyMars * math.pow(
        teff - 5780, 3) + cdEarlyMars * math.pow(teff - 5780, 4)
    EarlyMars = ((round((EarlyMars - math.floor(EarlyMars)) * 1000)) / 1000) + math.floor(EarlyMars)
    data.append(EarlyMars)
    SERunawayG = SEsRunawayG + SEcaRunawayG * (teff - 5780) + SEcbRunawayG * math.pow(teff - 5780,
                                                                                      2) + SEccRunawayG * math.pow(
        teff - 5780, 3) + SEcdRunawayG * math.pow(teff - 5780, 4)
    SERunawayG = ((round((SERunawayG - math.floor(SERunawayG)) * 10000)) / 10000) + math.floor(SERunawayG)
    data.append(SERunawayG)
    SubRunawayG = SubsRunawayG + SubcaRunawayG * (teff - 5780) + SubcbRunawayG * math.pow(teff - 5780,
                                                                                          2) + SubccRunawayG * math.pow(
        teff - 5780, 3) + SubcdRunawayG * math.pow(teff - 5780, 4)
    SubRunawayG = ((round((SubRunawayG - math.floor(SubRunawayG)) * 10000)) / 10000) + math.floor(SubRunawayG)
    data.append(SubRunawayG)

    recentVdis = math.sqrt(lum / recentV)
    recentVdis = ((round((recentVdis - math.floor(recentVdis)) * 1000)) / 1000) + math.floor(recentVdis)
    data.append(recentVdis)
    RunawayGdis = math.sqrt(lum / RunawayG)
    RunawayGdis = ((round((RunawayGdis - math.floor(RunawayGdis)) * 1000)) / 1000) + math.floor(RunawayGdis)
    data.append(RunawayGdis)
    MaximumGdis = math.sqrt(lum / MaximumG)
    MaximumGdis = ((round((MaximumGdis - math.floor(MaximumGdis)) * 1000)) / 1000) + math.floor(MaximumGdis)
    data.append(MaximumGdis)
    EarlyMarsdis = math.sqrt(lum / EarlyMars)
    EarlyMarsdis = ((round((EarlyMarsdis - math.floor(EarlyMarsdis)) * 1000)) / 1000) + math.floor(EarlyMarsdis)
    data.append(EarlyMarsdis)
    SERunawayGdis = math.sqrt(lum / SERunawayG)
    SERunawayGdis = ((round((SERunawayGdis - math.floor(SERunawayGdis)) * 1000)) / 1000) + math.floor(SERunawayGdis)
    data.append(SERunawayGdis)
    SubRunawayGdis = math.sqrt(lum / SubRunawayG)
    SubRunawayGdis = ((round((SubRunawayGdis - math.floor(SubRunawayGdis)) * 1000)) / 1000) + math.floor(SubRunawayGdis)
    data.append(SubRunawayGdis)

    return data[6:10]