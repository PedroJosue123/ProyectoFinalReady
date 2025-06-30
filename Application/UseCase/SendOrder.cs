using Application.IUseCase;
using Application.Mappers;
using Domain.Dtos;
using Domain.Entities;
using Domain.Interface;
using Infraestructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCase;

public class SendOrder  (IUnitOfWork unitOfWork) :ISendOrder
{
    public async Task<int> EnviarProducto(int id ,SendProductDto sendProductDto)
    {
        var preparacion = await unitOfWork.Repository<Preparacion>().GetByIdAsync(id);

        if (preparacion == null) throw new Exception("No se ha preparado Producto");

        var envioDomain = new SendProductDomain(0, sendProductDto.NombreEmpresa, sendProductDto.RucEmpresa, sendProductDto.Asesor
        , sendProductDto.NumeroTelefonico, sendProductDto.DireccionEnvio, sendProductDto.DireccionRecojo , sendProductDto.FechaLlegada,
        sendProductDto.NroGuia);


        var envioEntity = SendProductMapper.ToEntity(envioDomain);
        
        try
        {
            await unitOfWork.BeginTransactionAsync();

            // Guardamos primero el usuario
            await unitOfWork.Repository<Envio>().AddAsync(envioEntity);
            await unitOfWork.SaveChange();

            // Asociamos el ID generado al perfil
            preparacion.IdEnvio = envioEntity.IdEnvio;

            // Guardamos el perfil
           
            await unitOfWork.SaveChange();

            await unitOfWork.CommitTransactionAsync();
            return (int)preparacion.IdEnvio;
        }
        catch
        {
            await unitOfWork.RollbackTransactionAsync();
            throw new Exception("Datos Incompletos");

        }
    }

    public async Task<bool> ConfirmarEnvio(int id)
    {
        var envio = await unitOfWork.Repository<Envio>().GetByIdAsync(id);
        
        
        envio.Llegada = true;


        await unitOfWork.SaveChange();
        return true;
    }
    
    public async Task<GetSendOrderDomain> verEnvio(int id)
    {
        var preparacion = await unitOfWork.Repository<Preparacion>().GetAll().
            Where(u => u.IdPreparacion == id && u.Estado == true).Include(u => u.IdEnvioNavigation).FirstAsync();


        var envioDomain = GetSendOrderMapper.ToDomain(preparacion);
        
        return envioDomain;
    }



}