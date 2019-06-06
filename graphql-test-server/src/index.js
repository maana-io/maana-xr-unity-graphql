const { ApolloServer, gql } = require("apollo-server");

const questions = [
  {
    fact: "JavaScript is an excellent programming language",
    isTrue: true
  },
  {
    fact: "Scala is an excellent programming language",
    isTrue: false
  },
  {
    fact: "C is an excellent programming language",
    isTrue: true
  },
  {
    fact: "ML is an excellent programming language",
    isTrue: true
  }
];

const typeDefs = gql`
  type Question {
    fact: String
    isTrue: Boolean
  }
  type Query {
    questions: [Question]
  }
`;

const resolvers = {
  Query: {
    questions: () => questions
  }
};

const server = new ApolloServer({ typeDefs, resolvers });

server.listen().then(({ url }) => {
  console.log(`🚀  Server ready at ${url}`);
});
