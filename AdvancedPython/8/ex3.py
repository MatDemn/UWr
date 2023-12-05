'''
    Mateusz Zajac (298654)
    Advanced Python
    List 8
    Ex 3
'''

# Import sqlalchemy components and sys.argv
from sqlalchemy import create_engine, Column, String, Integer, ForeignKey, or_
from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy.orm import relationship, sessionmaker
import sys

# Basic setup for sqlalchemy ORM
base = declarative_base()
engine = create_engine("sqlite:///databaseFile.db")
sessionMaker = sessionmaker(bind=engine)

# Model for Movie
class MovieModel(base):
    __tablename__ = "Movie"

    id = Column(Integer, primary_key=True)
    title = Column(String)
    year = Column(Integer)

    id_director = Column(Integer, ForeignKey('Human.id'))
    id_cinematographer = Column(Integer, ForeignKey('Human.id'))
    id_producer = Column(Integer, ForeignKey('Human.id'))

    director = relationship('HumanModel', foreign_keys=[id_director])
    cinematographer = relationship('HumanModel', foreign_keys=[id_cinematographer])
    producer = relationship('HumanModel', foreign_keys=[id_producer])

    def __repr__(self):
        return f"<Movie(id={self.id}, title={self.title}, year={self.year}, director={self.director}, cinematographer={self.cinematographer}, producer={self.producer})>"
        
# Model for Human
class HumanModel(base):
    __tablename__ = "Human"

    id = Column(Integer, primary_key=True)
    name = Column(String)

    def __repr__(self):
        return f"<Human(id={self.id}, name={self.name})>"
  
# Make all models
base.metadata.create_all(bind=engine)

# Show all movies
def show_movies():
    with sessionMaker() as session:
        res = session.query(MovieModel).all()
        for r in res:
            print(r)

# Add movie with args properties
# Check if all needed humans exists in database
def add_movie(args):
    title = args[0]
    year = args[1]
    director_n = args[2]
    cinematographer_n = args[3]
    producer_n = args[4]
    with sessionMaker() as session:
        result = session.query(HumanModel).filter(
            or_(HumanModel.name == director_n,or_(HumanModel.name == cinematographer_n, HumanModel.name == producer_n)))

    director, cinematographer, producer = "", "", ""
    for person in result:
        if person.name == director_n:
            director = person
        if person.name == cinematographer_n:
            cinematographer = person
        if person.name == producer_n:
            producer = person
    if director and cinematographer and producer:
        m = MovieModel(title=title, year=year, id_director=director.id, id_cinematographer=cinematographer.id, id_producer=producer.id)
        with sessionMaker() as session:
            session.add(m)
            session.commit()
    else:
        print("Not all humans exists in database!")
        return

# Delete movie by title
def del_movie(title):
    with sessionMaker() as session:
        session.query(MovieModel).filter(MovieModel.title == title).delete()
        session.commit()

# Show all humans  
def show_humans():
    with sessionMaker() as session:
        res = session.query(HumanModel).all()
        for r in res:
            print(r)

# Add human with name. If exists, return
def add_human(name):
    with sessionMaker() as session:
        check_human = session.query(HumanModel).filter(HumanModel.name == name).first() 
        if check_human:
            print("Human with this name already exists!")
            return
        h = HumanModel(name=name)
        session.add(h)
        session.commit()

# Delete human with name
def del_human(name):
    with sessionMaker() as session:
        session.query(HumanModel).filter(HumanModel.name == name).delete()
        session.commit()

# If script was invoked as main script, not import
if __name__ == '__main__':
    if len(sys.argv) < 3:
        raise Exception("Not enough arguments provided...")
    if sys.argv[1] == 'movie':
        if sys.argv[2] == '--show':
            show_movies()
        elif sys.argv[2] == '--add':
            if len(sys.argv) < 8:
                raise Exception("Not enough argument for add passed...") 
            add_movie(sys.argv[3:])
        elif sys.argv[2] == '--delete':
            if len(sys.argv) < 4:
                raise Exception("Not enough arguments provided...")
            del_movie(sys.argv[3])
        else:
            raise Exception("Wrong argument at position 2. Correct are: --show, --add, --delete")

    elif sys.argv[1] == 'human':
        if sys.argv[2] == '--show':
            show_humans()
        elif sys.argv[2] == '--add':
            if len(sys.argv) < 4:
                raise Exception("Not enough arguments provided...")
            add_human(sys.argv[3])
        elif sys.argv[2] == '--delete':
            if len(sys.argv) < 4:
                raise Exception("Not enough arguments provided...")
            del_human(sys.argv[3]) 
        else:
            raise Exception("Wrong argument at position 2. Correct are: --show, --add, --delete") 

    else:
        raise Exception("Wrong argument at position 1. Correct are: movie, human")
