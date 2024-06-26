﻿using MeasurementAPI.Repositories;
using ShareModels;

namespace MeasurementAPI.Services;

public class MeasurementService: IMeasurementService
{
    private readonly IMeasurementRepository _repository;
    
    public MeasurementService(IMeasurementRepository repository)
    {
        _repository = repository;
    }
    
    public Measurement InsertMeasurement(MeasurementPostDTO measurementPostDTO)
    {
        Measurement measurement = new Measurement
        {
            date = measurementPostDTO.date,
            Systolic = measurementPostDTO.Systolic,
            Diastolic = measurementPostDTO.Diastolic,
            patientSSN = measurementPostDTO.patientSSN
        };
        
        return _repository.InsertMeasurement(measurement);
    }

    public Measurement UpdateMeasurement(Measurement measurement)
    {
        return _repository.UpdateMeasurement(measurement);
    }

    public async Task<List<Measurement>> GetMeassurements(string ssn)
    {
        return await _repository.GetMeassurements(ssn);
    }
}