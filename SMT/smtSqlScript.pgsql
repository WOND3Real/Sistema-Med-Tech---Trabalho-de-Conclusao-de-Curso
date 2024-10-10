CREATE TABLE Medico (
  crmMed VARCHAR(7)   NOT NULL ,
  nomeMed VARCHAR(45)   NOT NULL ,
  sobrenomeMed VARCHAR(45)   NOT NULL ,
  telefoneMed VARCHAR(14)   NOT NULL ,
  emailMed VARCHAR(45)   NOT NULL ,
  senhaMed VARCHAR(20)   NOT NULL   ,
PRIMARY KEY(crmMed));

CREATE TABLE Especialidade (
  idEspecialidade INTEGER   NOT NULL ,
  nomeEspec INTEGER   NOT NULL   ,
PRIMARY KEY(idEspecialidade));

CREATE TABLE Unidade (
  idUnidade INTEGER   NOT NULL ,
  nomeUnidade VARCHAR(20)   NOT NULL ,
  cepUni VARCHAR(10)   NOT NULL ,
  logradouroUni VARCHAR(45)   NOT NULL ,
  numeroUni VARCHAR(6)   NOT NULL ,
  bairroUni VARCHAR(20)   NOT NULL ,
  cidadeUni VARCHAR(20)   NOT NULL ,
  estadoUni VARCHAR(20)   NOT NULL ,
  paisUni VARCHAR(20)   NOT NULL   ,
PRIMARY KEY(idUnidade));

CREATE TABLE Paciente (
  cpfPaci VARCHAR(14)   NOT NULL ,
  nomePaci VARCHAR(45)   NOT NULL ,
  sobrenomePaci VARCHAR(45)   NOT NULL ,
  nascimentoPaci DATE   NOT NULL ,
  generoPaci genero   NOT NULL ,
  emailPaci VARCHAR(45)   NOT NULL ,
  telefonePaci VARCHAR(14)   NOT NULL ,
  senhaPaci VARCHAR(20)   NOT NULL   ,
PRIMARY KEY(cpfPaci));

CREATE TABLE Atendente (
  ctpsAtend VARCHAR(15)   NOT NULL ,
  nomeAtend VARCHAR(45)   NOT NULL ,
  sobrenomeAtend VARCHAR(45)   NOT NULL ,
  inicioTurnoAtend TIME   NOT NULL ,
  fimTurnoAtend TIME   NOT NULL ,
  senhaAtend VARCHAR(20)   NOT NULL   ,
PRIMARY KEY(ctpsAtend));

CREATE TABLE Administrador (
  idAdministrador INTEGER   NOT NULL ,
  Unidade_idUnidade INTEGER   NOT NULL ,
  nomeAdm VARCHAR(20)    ,
  sobrenomeAdm VARCHAR(20)    ,
  senhaAdm VARCHAR(20)      ,
PRIMARY KEY(idAdministrador, Unidade_idUnidade)  ,
  FOREIGN KEY(Unidade_idUnidade)
    REFERENCES Unidade(idUnidade));

CREATE TABLE rel_med_unidade (
  Medico_crmMed VARCHAR(7)   NOT NULL ,
  Unidade_idUnidade INTEGER   NOT NULL   ,
PRIMARY KEY(Medico_crmMed, Unidade_idUnidade)    ,
  FOREIGN KEY(Medico_crmMed)
    REFERENCES Medico(crmMed),
  FOREIGN KEY(Unidade_idUnidade)
    REFERENCES Unidade(idUnidade));

CREATE TABLE rel_uni_espec (
  Unidade_idUnidade INTEGER   NOT NULL ,
  Especialidade_idEspecialidade INTEGER   NOT NULL   ,
PRIMARY KEY(Unidade_idUnidade, Especialidade_idEspecialidade)    ,
  FOREIGN KEY(Unidade_idUnidade)
    REFERENCES Unidade(idUnidade),
  FOREIGN KEY(Especialidade_idEspecialidade)
    REFERENCES Especialidade(idEspecialidade));

CREATE TABLE Disponibilidade (
  Medico_crmMed VARCHAR(7)   NOT NULL ,
  Unidade_idUnidade INTEGER   NOT NULL ,
  dispoDom dispodia   NOT NULL ,
  dispoSeg dispodia   NOT NULL ,
  dispoTer dispodia   NOT NULL ,
  dispoQua dispodia   NOT NULL ,
  dispoQui dispodia   NOT NULL ,
  dispoSex dispodia   NOT NULL ,
  dispoSab dispodia   NOT NULL   ,
PRIMARY KEY(Medico_crmMed, Unidade_idUnidade)    ,
  FOREIGN KEY(Medico_crmMed)
    REFERENCES Medico(crmMed),
  FOREIGN KEY(Unidade_idUnidade)
    REFERENCES Unidade(idUnidade));

CREATE TABLE rel_med_espec (
  Medico_crmMed VARCHAR(7)   NOT NULL ,
  Especialidade_idEspecialidade INTEGER   NOT NULL   ,
PRIMARY KEY(Medico_crmMed, Especialidade_idEspecialidade)    ,
  FOREIGN KEY(Medico_crmMed)
    REFERENCES Medico(crmMed),
  FOREIGN KEY(Especialidade_idEspecialidade)
    REFERENCES Especialidade(idEspecialidade));

CREATE TABLE Consulta (
  idConsulta INTEGER   NOT NULL ,
  Paciente_cpfPaci VARCHAR(14)   NOT NULL ,
  Atendente_ctpsAtend VARCHAR(15)   NOT NULL ,
  Medico_crmMed VARCHAR(7)   NOT NULL ,
  Unidade_idUnidade INTEGER   NOT NULL ,
  Especialidade_idEspecialidade INTEGER   NOT NULL ,
  dataConsul DATE   NOT NULL ,
  horaConsul TIME   NOT NULL ,
  statusConsul statusconsulta   NOT NULL   ,
PRIMARY KEY(idConsulta, Paciente_cpfPaci, Atendente_ctpsAtend, Medico_crmMed, Unidade_idUnidade, Especialidade_idEspecialidade)          ,
  FOREIGN KEY(Paciente_cpfPaci)
    REFERENCES Paciente(cpfPaci),
  FOREIGN KEY(Atendente_ctpsAtend)
    REFERENCES Atendente(ctpsAtend),
  FOREIGN KEY(Medico_crmMed)
    REFERENCES Medico(crmMed),
  FOREIGN KEY(Unidade_idUnidade)
    REFERENCES Unidade(idUnidade),
  FOREIGN KEY(Especialidade_idEspecialidade)
    REFERENCES Especialidade(idEspecialidade));