﻿using AutoMapper;
using ProjectManagment.Application.Dtos;
using ProjectManagment.Application.Interfaces.Repositories;
using ProjectManagment.Application.Interfaces.Services;
using ProjectManagment.Domain.Entities;

namespace ProjectManagment.Application.Services;

public class EmployeeService: IEmployeeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<List<EmployeeResponse>> GetAllEmployeesAsync(int page, int pageSize)
    {
        var employee = await _unitOfWork.Employees.GetAll(page, pageSize);
        return employee.Select(_mapper.Map<EmployeeResponse>).ToList();
    }

    public async Task<EmployeeResponse> GetEmployeeByIdAsync(int id)
    {
        var employee = await _unitOfWork.Employees.GetById(id);
        return _mapper.Map<EmployeeResponse>(employee);
    }

    public async Task CreateEmployeeAsync(CreateEmployeeDto employee)
    {
        var employeeEntity = _mapper.Map<Employee>(employee);
        if(await _unitOfWork.Employees.FindByEmail(employee.Email) != null)
            throw new Exception($"Email {employee.Email} already exists");
            
        await _unitOfWork.Employees.Add(employeeEntity);
        await _unitOfWork.SaveAsync();
    }

    public async Task UpdateEmployeeAsync(UpdateEmployeeDto employee)
    {
        var employeeEntity = _mapper.Map<Employee>(employee);
        if(await _unitOfWork.Employees.FindByEmail(employee.Email) != null)
            throw new Exception($"Email {employee.Email} already exists");
        
        await _unitOfWork.Employees.Update(employeeEntity);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteEmployeeAsync(int employeeId)
    {
        var employeeEntity = await _unitOfWork.Employees.GetById(employeeId);
        _unitOfWork.Employees.Delete(employeeEntity);
        await _unitOfWork.SaveAsync();
    }
}