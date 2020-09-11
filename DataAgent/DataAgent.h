#pragma once

#include <QtWidgets/QMainWindow>
#include "ui_DataAgent.h"

class DataAgent : public QMainWindow
{
    Q_OBJECT

public:
    DataAgent(QWidget *parent = Q_NULLPTR);

private:
    Ui::DataAgentClass ui;
};
