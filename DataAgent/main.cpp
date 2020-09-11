#include "DataAgent.h"
#include <QtWidgets/QApplication>

int main(int argc, char *argv[])
{
    QApplication a(argc, argv);
    DataAgent w;
    w.show();
    return a.exec();
}
